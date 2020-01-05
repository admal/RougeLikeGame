using System.Collections;
using System.Collections.Generic;
using MazeGeneration.Rooms;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.RoomTests
{
    public class RoomPresetServiceTests
    {
        private RoomPresetService _sut;


        [Test]
        public void GetCorrectRoomPossibleCodes()
        {
            _sut = new RoomPresetService();

            var possibleCodes = _sut.GetPossibleCodes(15);

            foreach(var code in possibleCodes)
            {
                Debug.Log(code);
            }
        }
    }
}
