using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MazeGeneration
{
    public class MazeGenerator
    {
        private int _depth;
        private float _forwardTrackingProbability;
        private int _maxRoomsCount;
        private int _minRoomsCount;

        private MazeGenerator()
        {

        }

        public static MazeGenerator New()
        {
            return new MazeGenerator();
        }

        public MazeGenerator WithDepth(int depth)
        {
            _depth = depth;
            return this;
        }

        public MazeGenerator WithForwardTrackingProbability(float forwardTrackingProbability)
        {
            _forwardTrackingProbability = forwardTrackingProbability;
            return this;
        }

        public MazeGenerator WithRoomCountBoundaries(int minRooms, int maxRooms)
        {
            _minRoomsCount = minRooms;
            _maxRoomsCount = maxRooms;
            return this;
        }

        public RawMaze Generate()
        {
            var maze = new RawMaze();
            maze.Generate(_depth, _forwardTrackingProbability, _minRoomsCount, _maxRoomsCount);
            return maze;
        }
    }

}