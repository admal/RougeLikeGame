using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MazeGeneration;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.MazeTests
{
    public class MazeGetDeepestRoomTests
    {
        private RawMaze _rawMaze;

        public void Init()
        {
            //arrange
            //    1
            //  21012
            //    1 
            _rawMaze = new RawMaze();
            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX, RawMaze.InitialNodeY].Visited = true;
            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX, RawMaze.InitialNodeY].Depth = 0;

            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX, RawMaze.InitialNodeY - 1].Visited = true;
            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX, RawMaze.InitialNodeY - 1].Depth = 1;

            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX, RawMaze.InitialNodeY + 1].Visited = true;
            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX, RawMaze.InitialNodeY + 1].Depth = 1;

            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX - 1, RawMaze.InitialNodeY].Visited = true;
            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX - 1, RawMaze.InitialNodeY].Depth = 1;

            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX + 1, RawMaze.InitialNodeY].Visited = true;
            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX + 1, RawMaze.InitialNodeY].Depth = 1;

            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX - 2, RawMaze.InitialNodeY].Visited = true;
            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX - 2, RawMaze.InitialNodeY].Depth = 2;

            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX + 2, RawMaze.InitialNodeY].Visited = true;
            _rawMaze.GeneratedMaze[RawMaze.InitialNodeX + 2, RawMaze.InitialNodeY].Depth = 2;
        }

        [Test]
        public void GetFirstDeepestNode()
        {
            //arrange
            Init();

            //act
            var nodePostion = _rawMaze.GetDeepestNode(false);
            var node = _rawMaze.GeneratedMaze[nodePostion.X, nodePostion.Y];

            //assert
            Assert.AreEqual(RawMaze.InitialNodeX - 2, nodePostion.X);
            Assert.AreEqual(RawMaze.InitialNodeY, nodePostion.Y);
            Assert.AreEqual(2, node.Depth);
            Assert.IsTrue(node.Visited);
        }


        [Test]
        public void GetRandomDeepestNode()
        {
            //arrange
            Init();

            //act
            var nodePostion = _rawMaze.GetDeepestNode(true);
            var node = _rawMaze.GeneratedMaze[nodePostion.X, nodePostion.Y];

            //assert
            var isInPosition = nodePostion.Equals(new MazePosition(RawMaze.InitialNodeX - 2, RawMaze.InitialNodeY)) ||
                                nodePostion.Equals(new MazePosition(RawMaze.InitialNodeX + 2, RawMaze.InitialNodeY));
            
            Assert.IsTrue(isInPosition);
            Assert.AreEqual(2, node.Depth);
            Assert.IsTrue(node.Visited);
        }
    }
}
