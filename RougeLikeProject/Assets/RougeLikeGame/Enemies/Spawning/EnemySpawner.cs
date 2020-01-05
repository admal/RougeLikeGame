using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameManagers;
using UnityEngine;

namespace Enemies.Spawning
{
    public enum EnemyType
    {
        Shooting,
        Melee,
        Stationary,
        Flying,
        Walking,

    }
    public class EnemySpawner : MonoBehaviour
    {
        public List<EnemyType> AvailableEnemyTypes;

        public  IEnumerable<GameObject> SpawnRandomEnemy()
        {
            return SpawnRandomEnemy(1);
        }

        public IEnumerable<GameObject> SpawnRandomEnemy(int enemiesToSpawnCount)
        {
            var enemyTypes = AvailableEnemyTypes;
            var enemiesToSpawn = new List<GameObject>();

            for (int i = 0; i < enemiesToSpawnCount; i++)
            {
                var rnd = UnityEngine.Random.Range(0, enemyTypes.Count);
                var randomEnemyType = enemyTypes[rnd];

                var enemyPresets = EnemiesManager.Instance.GetEnemiesOfType(randomEnemyType);
                var rndEnemy = UnityEngine.Random.Range(0, enemyPresets.Count);

                enemiesToSpawn.Add(enemyPresets[rndEnemy]);
            }

            foreach (var enemy in enemiesToSpawn)
            {
                var spawnedEnemy = GameObject.Instantiate(enemy, transform.position, Quaternion.identity);
                //spawnedEnemy.GetComponent //TODO: make cool spawning
                yield return spawnedEnemy;
            }

        }
    }
}