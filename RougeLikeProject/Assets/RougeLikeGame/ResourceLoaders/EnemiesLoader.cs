using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoaders
{
    public class EnemiesLoader
    {
        public GameObject[] LoadAllEnemies()
        {
            var enemiesPresets = Resources.LoadAll<GameObject>("Enemies/Presets");
            return enemiesPresets;
        }
    }
}