using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagers
{
    public class RoomManager
    {
        public const float RoomWidth = 4f;
        public const float RoomHeight = 4f;
        private static RoomManager _instance;

        private GameObject[][] _roomPresets;
        private GameObject _bossRoom;
        private GameObject _initialRoom;

        public void SetRoomPresets(GameObject[][] roomPresets)
        {
            _roomPresets = roomPresets;
        }

        public GameObject[] GetRoomPresets(int code)
        {
            return _roomPresets[code - 1];
        }

        public void SetBossRoom(GameObject bossRoom)
        {
            _bossRoom = bossRoom;
        }

        public GameObject GetBossRoom()
        {
            return _bossRoom;
        }

        public void SetInitialRoom(GameObject initialRoom)
        {
            _initialRoom = initialRoom;
        }

        public GameObject GetInitialRoom()
        {
            return _initialRoom;
        }

        public bool IsLoaded => _roomPresets != null && _roomPresets.Length > 0;

        public static RoomManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RoomManager();
                }

                return _instance;
            }
        }

        private RoomManager() { }
    }
}