using System.Collections;
using System.Collections.Generic;
using ResourceLoaders;
using UnityEngine;

namespace GameManagers
{
    public class ResourceManager
    {
        private static ResourceManager _instance;
        public static ResourceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ResourceManager();
                }
                return _instance;
            }
        }

        private ResourceManager() { }

        public void LoadRoomPresets()
        {
            if (RoomManager.Instance.IsLoaded)
            {
                return;
            }

            var loader = new RoomsLoader();
            var presets = loader.LoadAllRoomPresets();
            RoomManager.Instance.SetRoomPresets(presets);

            RoomManager.Instance.SetBossRoom(loader.LoadBossRoom());
        }

        public void LoadEnemiesPresets()
        {
            Debug.Log("Load enemies presets");
            var loader = new EnemiesLoader();
            var presets = loader.LoadAllEnemies();
            foreach (var enemyPreset in presets)
            {
                EnemiesManager.Instance.AddEnemy(enemyPreset);
            }  
        }
    }
}