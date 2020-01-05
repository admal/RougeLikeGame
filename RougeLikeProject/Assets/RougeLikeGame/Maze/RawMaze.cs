
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MazeGeneration
{
    public class RawMaze
    {
        private MazeNode[,] _generatedMaze;
        public MazeNode[,] GeneratedMaze => _generatedMaze;

        private MazePosition _initialPosition;
        public const int InitialNodeX = 25;
        public const int InitialNodeY = 25;
        public RawMaze()
        {
            _initialPosition = new MazePosition(InitialNodeX, InitialNodeY);
            _generatedMaze = new MazeNode[50, 50];
            for (int i = 0; i < _generatedMaze.GetLength(0); i++)
            {
                for (int j = 0; j < _generatedMaze.GetLength(1); j++)
                {
                    _generatedMaze[i, j] = new MazeNode(MazeRoomType.Normal);
                }
            }
            _generatedMaze[InitialNodeX, InitialNodeY].Visited = true;
            _generatedMaze[InitialNodeX, InitialNodeY].RoomType = MazeRoomType.Initial;
        }


        public MazeNode At(int x, int y)
        {
            return _generatedMaze[x, y];
        }

        public MazeNode At(MazePosition position)
        {
            return At(position.X, position.Y);
        }

        private IEnumerable<MazePosition> GetAvailablePositions(MazePosition fromPosition)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (fromPosition.X + i >= _generatedMaze.GetLength(0) || fromPosition.Y + j >= _generatedMaze.GetLength(1))
                    {
                        continue;
                    }
                    if (fromPosition.X + i < 0 || fromPosition.Y + j < 0)
                    {
                        continue;
                    }
                    if (i * j > 0 || i * j < 0)
                    {
                        continue;
                    }

                    var curr = _generatedMaze[fromPosition.X + i, fromPosition.Y + j];
                    if (curr.Visited == false)
                    {
                        yield return new MazePosition(fromPosition.X + i, fromPosition.Y + j);
                    }
                }
            }
        }

        public void Generate(int maxDepth, float forwardPropagationProbability, int minRoomsCount, int maxRoomsCount)
        {
            var callStack = new Stack<MazePosition>();
            var currentPosition = _initialPosition;
            callStack.Push(currentPosition);
            var roomsCount = 1;

            while (true)
            {
                // || roomsCount < minRoomsCount 
                //TODO: FIX THAT STUFF WITH callStack.Count == 1 && availablePositions.Count() == 0
                var doForwardPropagation = Random.value < forwardPropagationProbability || callStack.Count == 1;
                // Debug.Log($"Do forward propagation: {doForwardPropagation}.");
                var currentRoom = _generatedMaze[currentPosition.X, currentPosition.Y];

                var availablePositions = GetAvailablePositions(currentPosition).ToArray();
                // Debug.Log($"Available positions count: {availablePositions.Count()}.");

                if (callStack.Count <= 1 && availablePositions.Count() == 0)
                {
                    Debug.LogError("Call stack is empty and no available positions.");
                    return;
                }

                if (doForwardPropagation && availablePositions.Count() > 0)
                {
                    // Debug.Log($"Forward propagation");
                    //forward propagation
                    var nextPositionIdx = Random.Range(0, availablePositions.Count());
                    var nextPosition = availablePositions[nextPositionIdx];
                    // Debug.Log($"Next position: {nextPosition}.");
                    var nextRoom = _generatedMaze[nextPosition.X, nextPosition.Y];
                    nextRoom.Visited = true;
                    nextRoom.Depth = currentRoom.Depth + 1;

                    roomsCount++;

                    callStack.Push(nextPosition);
                }
                else
                {
                    //do backpropagation
                    // Debug.Log($"Backpropagation");
                    currentPosition = callStack.Pop();
                }

                var end = Random.value < 0.5f && roomsCount >= minRoomsCount;
                if (end || roomsCount >= maxRoomsCount)
                {
                    // Debug.Log($"Return");
                    return;
                }
            }
        }

        /// <summary>
        /// Get the deepest node position in maze. If there are several nodes on the same level than return the first one or random one.
        /// </summary>
        /// <param name="randomDeepest">If true return random deepest node position</param>
        /// <returns></returns>
        public MazePosition GetDeepestNode(bool randomDeepest)
        {
            var maxDeepnes = -1;
            var deepestNodesPositions = new List<MazePosition>();
            for (int i = 0; i < _generatedMaze.GetLength(0); i++)
            {
                for (int j = 0; j < _generatedMaze.GetLength(1); j++)
                {
                    var mazeNode = _generatedMaze[i, j];
                    
                    if (mazeNode.Visited == false)
                    {
                        continue;
                    }

                    if (mazeNode.Depth > maxDeepnes)
                    {
                        maxDeepnes = mazeNode.Depth;
                        deepestNodesPositions.Clear();
                        deepestNodesPositions.Add(new MazePosition(i, j));
                    }
                    else if (mazeNode.Depth == maxDeepnes)
                    {
                        deepestNodesPositions.Add(new MazePosition(i, j));
                    }
                }
            }

            if (randomDeepest)
            {
                var randomNodeIdx = Random.Range(0, deepestNodesPositions.Count);
                return deepestNodesPositions[randomNodeIdx];
            }
            else
            {
                return deepestNodesPositions.First();
            }
        }


        //TODO: think if move it into other place
        public void AddBossRoom()
        {
            var deepestNodePostion = GetDeepestNode(true);
            var deepestNode = _generatedMaze[deepestNodePostion.X, deepestNodePostion.Y];

            var bossNode = new MazeNode(MazeRoomType.Boss);
            bossNode.Depth = deepestNode.Depth + 1;
            bossNode.Visited = true;

            //Check all possible neighbours and place boss room in the first one
            if (At(deepestNodePostion.X - 1, deepestNodePostion.Y).Visited == false)
            {
                _generatedMaze[deepestNodePostion.X - 1, deepestNodePostion.Y] = bossNode;
            } 
            else if (At(deepestNodePostion.X + 1, deepestNodePostion.Y).Visited == false)
            {
                _generatedMaze[deepestNodePostion.X + 1, deepestNodePostion.Y] = bossNode;
            }
            else if (At(deepestNodePostion.X, deepestNodePostion.Y - 1).Visited == false)
            {
                _generatedMaze[deepestNodePostion.X, deepestNodePostion.Y - 1] = bossNode;
            }
            else if (At(deepestNodePostion.X, deepestNodePostion.Y + 1).Visited == false)
            {
                _generatedMaze[deepestNodePostion.X, deepestNodePostion.Y + 1] = bossNode;
            }
            else
            {
                //This should never happen because if there is anything further than means it is not deepest node
                Debug.LogError("No available room for boss room.");
            }
        }
    }
}