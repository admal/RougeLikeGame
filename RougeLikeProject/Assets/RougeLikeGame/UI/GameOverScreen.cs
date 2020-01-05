using System;
using System.Collections;
using System.Collections.Generic;
using GameManagers;
using Player;
using UnityEngine;

namespace GameUI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField]
        private GameObject _deathScreen;

        [SerializeField]
        private GameObject _congratulationScreen;

        private bool _wasPlayerKilled = false;
        private bool _playerWon = false;

        private void Start()
        {
            PlayerManager.Instance.PlayerHealth.OnPlayerDeath += ShowGameOverDeathScreen;
            LevelManager.Instance.OnGameWon += ShowGameWonScreen;
        }

        private void ShowGameWonScreen()
        {
            _playerWon = true;
            _congratulationScreen.SetActive(true);
        }

        private void ShowGameOverDeathScreen()
        {
            _wasPlayerKilled = true;
            _deathScreen.SetActive(true);
        }

        private void Update()
        {
            if(_wasPlayerKilled || _playerWon)
            {
                if(Input.GetButton("Jump"))
                {
                    Debug.Log("Reset game");
                    GameManager.Instance.ResetGame();
                }
                else if(Input.GetButton("Cancel"))
                {
                    Debug.Log("Load menu scene");
                }
            }

        }
    }
}
