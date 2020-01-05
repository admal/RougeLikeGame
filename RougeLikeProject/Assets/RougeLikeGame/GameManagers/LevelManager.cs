using System;
using System.Collections;
using System.Collections.Generic;
using MazeGeneration;
using Player;
using UnityEngine;

namespace GameManagers
{
    public class LevelManager
    {
        private int _currentLevel = 0;
        public int CurrentLevel => _currentLevel;

        private GameObject _mazeObject;

        public event Action OnStartLoadingLevel = delegate { };
        public event Action OnFinishedLoadingLevel = delegate { };
        public event Action OnGameWon = delegate { };

        private static LevelManager _instance;
        public static LevelManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LevelManager();
                }
                return _instance;
            }
        }

        private LevelManager()
        {
            Reset();
        }

        public void GotoNextLevel()
        {
            //TODO: make it better
            if (_currentLevel == 3)
            {
                OnGameWon();
            }

            _currentLevel++;

            OnStartLoadingLevel();

            if (_mazeObject == null)
            {
                _mazeObject = GameObject.Find("Maze");
            }
            //TODO: delay creating new maze
            foreach (Transform child in _mazeObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            MazeGeneratorObject.Instance.CreateNewMaze(/*ADD PARAMS*/);
            PlayerManager.Instance.Position = new Vector3(RoomManager.RoomWidth * RawMaze.InitialNodeX, RoomManager.RoomHeight * RawMaze.InitialNodeY, 0);
            MazeGeneratorObject.Instance.CurrentRoom.EnterRoom();
            OnFinishedLoadingLevel();



            //DO GENERATION STUFF
        }

        public void Reset()
        {
            _currentLevel = 0;
        }
    }
}