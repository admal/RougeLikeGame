using System;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoaders
{
    public class RoomsLoader
    {
        public GameObject[][] LoadAllRoomPresets()
        {
            var presets = new GameObject[15][];

            //There are 15 combinations (from 1 to 15 inclusively) of room exists, so load all of them
            for (int i = 1; i <= 15; i++)
            {
                var currentBinaryPreset = Convert.ToString(i, 2).PadLeft(4, '0');
                var rooms = Resources.LoadAll<GameObject>("Rooms/Presets/" + currentBinaryPreset);
                presets[i - 1] = rooms;
            }

            return presets;
        }

        public GameObject LoadBossRoom()
        {
            var room = Resources.Load<GameObject>("Rooms/Boss/BossRoom");

            return room;
        }
    }
}