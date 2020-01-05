using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MazeGeneration;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.MazeTests
{
    public class MazeTests
    {
        private RawMaze _rawMaze;

        public void Init()
        {
            //arrange
            //    #
            //   ###
            //    # #
            _rawMaze = new RawMaze();
            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX, RawMaze.InitialNodeY - 1].Visited = true;
            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX, RawMaze.InitialNodeY + 1].Visited = true;
            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX - 1, RawMaze.InitialNodeY].Visited = true;
            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX + 1, RawMaze.InitialNodeY].Visited = true;
            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX + 2, RawMaze.InitialNodeY - 1].Visited = true;
        }

        [Test]
        public void ToMazeTest()
        {
            //arrange
            Init();

            //act
            var maze = _rawMaze.ToMaze();

            //assert
            Assert.AreEqual(6, maze.RoomCount);
            Assert.AreEqual(4, maze.CurrentRoom.Neighbours.Count);
            Assert.AreEqual(new MazePosition(RawMaze.InitialNodeX, RawMaze.InitialNodeY), maze.CurrentRoom.Position);
            Assert.AreEqual(new MazePosition(RawMaze.InitialNodeX - 1, RawMaze.InitialNodeY), maze.CurrentRoom.GetNeighbour(MazeRoomNeighbourPosition.Left).Position);
            Assert.AreEqual(new MazePosition(RawMaze.InitialNodeX + 1, RawMaze.InitialNodeY), maze.CurrentRoom.GetNeighbour(MazeRoomNeighbourPosition.Right).Position);
            Assert.AreEqual(new MazePosition(RawMaze.InitialNodeX, RawMaze.InitialNodeY + 1), maze.CurrentRoom.GetNeighbour(MazeRoomNeighbourPosition.Top).Position);
            Assert.AreEqual(new MazePosition(RawMaze.InitialNodeX, RawMaze.InitialNodeY - 1), maze.CurrentRoom.GetNeighbour(MazeRoomNeighbourPosition.Bottom).Position);
            Assert.AreEqual(
                null,
                maze.CurrentRoom.GetNeighbour(MazeRoomNeighbourPosition.Bottom).GetNeighbour(MazeRoomNeighbourPosition.Right)
            );
        }

        [Test]
        public void MovingToRoomAndBack()
        {
            //arrange
            Init();

            //act
            var maze = _rawMaze.ToMaze();
            var initialRoom = maze.CurrentRoom;
            
            maze.MoveToRoom(MazeRoomNeighbourPosition.Right);

            var nextRoom = maze.CurrentRoom;
            
            maze.MoveToRoom(MazeRoomNeighbourPosition.Left);
            var againInitialRoom = maze.CurrentRoom;

            //assert
            Assert.AreEqual(new MazePosition(RawMaze.InitialNodeX, RawMaze.InitialNodeY), initialRoom.Position);
            Assert.AreEqual(new MazePosition(RawMaze.InitialNodeX + 1, RawMaze.InitialNodeY), nextRoom.Position);
            Assert.AreEqual(new MazePosition(RawMaze.InitialNodeX, RawMaze.InitialNodeY), againInitialRoom.Position);
            Assert.AreEqual(initialRoom.Position, againInitialRoom.Position);
        }

        [Test]
        public void MovingToExactRoomPosition()
        {
            //arrange
            Init();

            //act
            var maze = _rawMaze.ToMaze();
            maze.MoveToRoom(new MazePosition(RawMaze.InitialNodeX + 2, RawMaze.InitialNodeY - 1));
            var room = maze.CurrentRoom;

            //assert
            Assert.AreEqual(new MazePosition(RawMaze.InitialNodeX + 2, RawMaze.InitialNodeY - 1), room.Position);
            Assert.AreEqual(0, room.Neighbours.Count);
            Assert.AreEqual(null, room.GetNeighbour(MazeRoomNeighbourPosition.Left));
            Assert.AreEqual(null, room.GetNeighbour(MazeRoomNeighbourPosition.Right));
            Assert.AreEqual(null, room.GetNeighbour(MazeRoomNeighbourPosition.Top));
            Assert.AreEqual(null, room.GetNeighbour(MazeRoomNeighbourPosition.Bottom));
        }

        [Test]
        public void MovingToExactRoom()
        {
            //arrange
            Init();

            //act
            var maze = _rawMaze.ToMaze();
            var initialRoom = maze.CurrentRoom;
            maze.MoveToRoom(MazeRoomNeighbourPosition.Right);
            
            maze.MoveToRoom(initialRoom);
            var movedTo = maze.CurrentRoom;

            //assert
            Assert.AreEqual(initialRoom.Position, movedTo.Position);            
        }
    }
}
