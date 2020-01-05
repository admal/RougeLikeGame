using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.Spawning;
using MazeGeneration;
using UnityEngine;

namespace GameManagers
{
    public class EnemiesManager
    {
        private static EnemiesManager _instance;
        public static EnemiesManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EnemiesManager();
                }

                return _instance;
            }
        }

        private EnemiesManager()
        {
            _enemyPresets = new Dictionary<EnemyType, List<GameObject>>();
        }

        private Dictionary<EnemyType,List<GameObject>> _enemyPresets;

        
        public void AddEnemy(GameObject enemyPreset)
        {
            var enemy = enemyPreset.GetComponent<EnemyBase>();
            foreach (var enemyType in enemy.Types)
            {
                if (_enemyPresets.ContainsKey(enemyType) == false)
                {
                    _enemyPresets.Add(enemyType, new List<GameObject>());
                }
                Debug.Log($"Add enemy {enemyType}");
                _enemyPresets[enemyType].Add(enemyPreset);
            }
        }

        public List<GameObject> GetEnemiesOfType(EnemyType enemyType)
        {
            return _enemyPresets[enemyType];
        }

    }
}
