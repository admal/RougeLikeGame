using System;
using System.Collections;
using System.Collections.Generic;
using MazeGeneration;
using UnityEngine;
using ResourceLoaders;
using GameManagers;
using System.Linq;

public class MazeGeneratorObject : MonoBehaviour
{
    public static MazeGeneratorObject Instance { get; private set; }
    private Maze _maze;
    public event Action OnMazeGenerated = delegate { };
    public event Action<MazeRoom, MazeRoom, MazeRoomNeighbourPosition> OnRoomChanged = delegate { };

    public MazeRoom CurrentRoom => _maze.CurrentRoom;

    [SerializeField]
    private GameObject _tile;
    private MazeGenerator _generator;

    private void Awake()
    {
        if (Instance != null)
        {
            throw new Exception("There is already created instance of MazeGenerator!!!!!");
        }
        Instance = this;
    }

    private void Start()
    {
        LevelManager.Instance.GotoNextLevel();
    }

    public void CreateNewMaze(/*Some params*/)
    {
        _maze = null;

        var maze = MazeGenerator
            .New()
            .WithDepth(15)
            .WithForwardTrackingProbability(0.3f)
            .WithRoomCountBoundaries(30, 40)
            .Generate();

        maze.AddBossRoom();
        var generateMaze = maze.GeneratedMaze;

        var width = RoomManager.RoomWidth;
        var height = RoomManager.RoomHeight;

        _maze = maze.ToMaze();

        for (int i = 0; i < generateMaze.GetLength(0); i++)
        {
            for (int j = 0; j < generateMaze.GetLength(1); j++)
            {
                var currentNode = generateMaze[i, j];
                if (currentNode.Visited)
                {
                    var spawnedRoom = Instantiate(_tile, new Vector3(i * width, j * height, 0), Quaternion.identity, transform);
                    var room = spawnedRoom.GetComponent<Room>();
                    room.MazeRoom = _maze.GetRoom(new MazePosition(i, j));
                }
            }
        }
        OnMazeGenerated();
    }

    public void MoveToRoom(MazeRoomNeighbourPosition to)
    {
        var previousRoom = _maze.CurrentRoom;
        _maze.MoveToRoom(to);
        var newRoom = _maze.CurrentRoom;
        newRoom.EnterRoom(to.Negative());
        previousRoom.ExitRoom();

        if (previousRoom.Position.Equals(newRoom.Position))
        {
            OnRoomChanged(previousRoom, newRoom, to);
        }
    }
}
