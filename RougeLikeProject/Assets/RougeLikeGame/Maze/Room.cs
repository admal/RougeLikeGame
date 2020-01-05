using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Doors;
using Enemies.Spawning;
using GameManagers;
using MazeGeneration;
using MazeGeneration.Rooms;
using Player;
using UnityEngine;

public class Room : MonoBehaviour
{
    public MazeRoom MazeRoom { get; set; }
    private GameObject _roomInstance;
    private IRoomPresetService _roomPresetService;
    private GameObject _vmCamera;
    private Dictionary<MazeRoomNeighbourPosition, Vector3> _doorsSpawnPoints;
    private List<Door> _doors;
    private List<EnemySpawner> _enemySpawners;

    private int _spawnedEnemiesCount = 0;

    private bool _isGenerated = false;
    void Awake()
    {
        _roomPresetService = new RoomPresetService();
        _vmCamera = transform.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>(true).gameObject;
        _doorsSpawnPoints = new Dictionary<MazeRoomNeighbourPosition, Vector3>();
        MazeGeneratorObject.Instance.OnMazeGenerated += OnMazeGenerated;
    }

    private void OnMazeGenerated()
    {
        if (_isGenerated)
        {
            return;
        }

        switch (MazeRoom.RoomType)
        {
            case MazeRoomType.Normal:
            case MazeRoomType.Initial: //TODO: tmp
                var possibleRooms = _roomPresetService.GetPossibleRoomPresets(MazeRoom.NeighboursCode);
                //Make random generator service
                var rnd = UnityEngine.Random.Range(0, possibleRooms.Count);

                _roomInstance = Instantiate(possibleRooms[rnd], transform);
                break;
            case MazeRoomType.Boss:
                _roomInstance = Instantiate(RoomManager.Instance.GetBossRoom(), transform);
                // _roomInstance.GetComponentInChildren<LevelDoor>().Open(); //TODO: VERY TMP FIX IT
                break;
            default:
                Debug.LogError($"{MazeRoom.RoomType} is not supported!");
                break;

        }

        _doors = _roomInstance.GetComponentsInChildren<Door>().ToList();
        _enemySpawners = _roomInstance.GetComponentsInChildren<EnemySpawner>().ToList();

        foreach (var door in _doors)
        {
            if (MazeRoom.Neighbours.ContainsKey(door.To) == false)
            {
                door.gameObject.SetActive(false);
            }
            else
            {
                _doorsSpawnPoints.Add(door.To, door.SpawnPoint);
            }
        }
        MazeRoom.OnEntered += OnRoomEntered;
        MazeRoom.OnExit += OnRoomLeft;
        _isGenerated = true;
    }


    private void OnRoomEntered(MazeRoomNeighbourPosition? from = null)
    {
        _vmCamera.SetActive(true);
        if (from.HasValue)
        {
            PlayerManager.Instance.Position = _doorsSpawnPoints[from.Value];
        }

        Debug.Log($"Player visited room: {MazeRoom.PlayerVisited}");

        if (MazeRoom.PlayerVisited)
        {
            OpenDoors();
        }

        if (MazeRoom.PlayerVisited == false)
        {
            foreach (var spawner in _enemySpawners)
            {
                var spawnedEnemies = spawner.SpawnRandomEnemy().ToList();
                _spawnedEnemiesCount += spawnedEnemies.Count;
                foreach (var spawnedEnemy in spawnedEnemies)
                {
                    var health = spawnedEnemy.GetComponent<Health.Health>();
                    health.OnDeath += OnEnemyKilled;
                }
            }
        }
    }

    private void OnEnemyKilled()
    {
        _spawnedEnemiesCount--;
        if (_spawnedEnemiesCount == 0)
        {
            OpenDoors();
        }
    }

    private void OpenDoors()
    {
        foreach (var door in _doors)
        {
            door.Open();
        }
    }

    private void OnRoomLeft()
    {
        _vmCamera.SetActive(false);
    }

    private void OnDestroy()
    {
        MazeRoom.OnEntered -= OnRoomEntered;
        MazeRoom.OnExit -= OnRoomLeft;
        MazeGeneratorObject.Instance.OnMazeGenerated -= OnMazeGenerated;
    }
}
