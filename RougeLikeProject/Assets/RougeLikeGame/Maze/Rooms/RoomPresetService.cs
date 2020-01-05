using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using GameManagers;
using UnityEngine;

namespace MazeGeneration.Rooms
{
    public interface IRoomPresetService
    {
        List<GameObject> GetPossibleRoomPresets(int roomNeigbourConfigurationCode);
    }

    public class RoomPresetService : IRoomPresetService
    {
        private Dictionary<int, List<int>> _allPossibleCodes = new Dictionary<int, List<int>>(){
                {1, new List<int>(){1, 3,5,7,9,11,13,15}},
                {2, new List<int>(){2, 3,6,7,10,11,14,15}},
                {3, new List<int>(){3, 7, 11, 15}},
                {4, new List<int>(){4, 5, 6, 7, 12,13,14,15}},
                {5, new List<int>(){5, 7, 13, 15}},
                {6, new List<int>(){6, 7, 14, 15}},
                {7, new List<int>(){7, 15}},
                {8, new List<int>(){8, 9, 10, 11, 12,13,14,15}},
                {9, new List<int>(){9, 11, 13, 15}},
                {10, new List<int>(){10, 11, 14, 15}},
                {11, new List<int>(){11, 15}},
                {12, new List<int>(){12, 13, 14, 15}},
                {13, new List<int>(){13, 15}},
                {14, new List<int>(){14, 15}},
                {15, new List<int>(){15}},
            };

        public List<int> GetPossibleCodes(int roomNeigbourConfigurationCode)
        {
            var possibleCodesToUse = _allPossibleCodes[roomNeigbourConfigurationCode];
            return possibleCodesToUse;
        }

        public List<GameObject> GetPossibleRoomPresets(int roomNeigbourConfigurationCode)
        {
            var possibleCodes = GetPossibleCodes(roomNeigbourConfigurationCode);
            var possibleRooms = new List<GameObject>();
            foreach (var possibleCode in possibleCodes)
            {
                var presets = RoomManager.Instance.GetRoomPresets(possibleCode);
                possibleRooms.AddRange(presets);
            }

            return possibleRooms;
        }
    }
}