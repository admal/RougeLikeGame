using System.Collections;
using System.Collections.Generic;
using Hash;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagers
{
    public class GameManager
    {
        public string Hash { get; private set; }
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        private GameManager() { }

        public void LoadPresets()
        {
            ResourceManager.Instance.LoadRoomPresets();
            ResourceManager.Instance.LoadEnemiesPresets();
        }

        public void InitHash(string hash = null)
        {
            var hashService = new HashService();
            if (string.IsNullOrEmpty(hash))
            {
                hash = hashService.InitWithRandomSeed();
            }
            else
            {
                hashService.InitWithSeed(hash);
            }

            Hash = hash;
        }

        public void ResetGame()
        {
            LevelManager.Instance.Reset();
            SceneManager.LoadScene("GameScene");
        }
    }
}