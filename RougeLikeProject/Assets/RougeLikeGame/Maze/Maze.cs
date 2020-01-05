using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace MazeGeneration
{
    public enum MazeRoomNeighbourPosition
    {
        Left,
        Right,
        Top,
        Bottom
    }

    public static class MazeRoomNeighbourPositionExtensions
    {
        //TODO: test it!!!!
        public static Vector2 ToOffset(this MazeRoomNeighbourPosition @this)
        {
            switch (@this)
            {
                case MazeRoomNeighbourPosition.Top:
                    return new Vector2(0, 1);
                case MazeRoomNeighbourPosition.Bottom:
                    return new Vector2(0, -1);
                case MazeRoomNeighbourPosition.Right:
                    return new Vector2(1, 0);
                case MazeRoomNeighbourPosition.Left:
                    return new Vector2(-1, 0);
            }

            throw new Exception("MazeRoomNeighbourPosition does not exists");
        }

        //TODO: test it!!!!
        public static int ToInt(this MazeRoomNeighbourPosition @this)
        {
            switch (@this)
            {
                case MazeRoomNeighbourPosition.Top:
                    return 8;
                case MazeRoomNeighbourPosition.Bottom:
                    return 2;
                case MazeRoomNeighbourPosition.Right:
                    return 4;
                case MazeRoomNeighbourPosition.Left:
                    return 1;
            }

            throw new Exception("MazeRoomNeighbourPosition does not exists");
        }

        public static MazeRoomNeighbourPosition Negative(this MazeRoomNeighbourPosition @this)
        {
            switch (@this)
            {
                case MazeRoomNeighbourPosition.Bottom:
                    return MazeRoomNeighbourPosition.Top;
                case MazeRoomNeighbourPosition.Top:
                    return MazeRoomNeighbourPosition.Bottom;
                case MazeRoomNeighbourPosition.Right:
                    return MazeRoomNeighbourPosition.Left;
                case MazeRoomNeighbourPosition.Left:
                    return MazeRoomNeighbourPosition.Right;
            }

            throw new Exception("MazeRoomNeighbourPosition does not exists");
        }
    }

    public class MazeRoom
    {
        public MazeRoom(MazePosition position, MazeRoomType roomType)
        {
            Position = position;
            RoomType = roomType;
            Neighbours = new Dictionary<MazeRoomNeighbourPosition, MazeRoom>();
        }
        public MazeRoomType RoomType { get; private set; }
        public event Action<MazeRoomNeighbourPosition?> OnEntered = delegate { };
        public event Action OnExit = delegate { };
        public MazePosition Position { get; private set; }
        public Dictionary<MazeRoomNeighbourPosition, MazeRoom> Neighbours { get; private set; }

        private int _neighboursCode;
        public int NeighboursCode => _neighboursCode;
        public void AddNeighbour(MazeRoomNeighbourPosition position, MazeRoom room)
        {
            _neighboursCode += position.ToInt();
            Neighbours.Add(position, room);
        }

        public MazeRoom GetNeighbour(MazeRoomNeighbourPosition position)
        {
            if (Neighbours.ContainsKey(position))
            {
                return Neighbours[position];
            }
            return null;
        }

        public void EnterRoom(MazeRoomNeighbourPosition? from = null)
        {
            OnEntered(from);
        }

        public void ExitRoom()
        {
            OnExit();
        }

        private bool _playerVisited = false;
        public bool PlayerVisited { get => _playerVisited; set => _playerVisited = value; }
    }

    public class Maze
    {
        private List<MazeRoom> _rooms;
        public int RoomCount => _rooms.Count;
        private MazeRoom _currentRoom;
        public MazeRoom CurrentRoom => _currentRoom;

        public Maze(List<MazeRoom> rooms, MazeRoom room)
        {
            _currentRoom = room;
            _rooms = rooms;
        }

        public void MoveToRoom(MazeRoomNeighbourPosition position)
        {
            var neighbour = _currentRoom.GetNeighbour(position);
            MoveToRoom(neighbour);
        }

        public void MoveToRoom(MazePosition position)
        {
            var room = _rooms.FirstOrDefault(x => x.Position.Equals(position));
            MoveToRoom(room);
        }

        public void MoveToRoom(MazeRoom room)
        {
            if (room != null)
            {
                //mark previous room as visited
                _currentRoom.PlayerVisited = true;
                //enter new room
                _currentRoom = room;
            }
        }

        public MazeRoom GetRoom(MazePosition position)
        {
            return _rooms.FirstOrDefault(x => x.Position.Equals(position));
        }
    }


    public static class RawMazeExtensions
    {
        public static Maze ToMaze(this RawMaze @this)
        {
            var rawMaze = @this.GeneratedMaze;
            Debug.Log($"RawMaze GetLength: {rawMaze.GetLength(0)}; GetLength: {rawMaze.GetLength(1)};");
            var rooms = new Dictionary<MazePosition, MazeRoom>();

            for (int i = 0; i < rawMaze.GetLength(0); i++)
            {
                for (int j = 0; j < rawMaze.GetLength(1); j++)
                {
                    if (rawMaze[i, j].Visited == false)
                    {
                        continue;
                    }
                    var position = new MazePosition(i, j);

                    var room = new MazeRoom(position, @this.At(position).RoomType);
                    rooms.Add(position, room);
                }
            }

            MazeRoom initialRoom = null;
            foreach (var roomElement in rooms)
            {
                var room = roomElement.Value;
                SetNeighbour(room, MazeRoomNeighbourPosition.Top, rooms);
                SetNeighbour(room, MazeRoomNeighbourPosition.Bottom, rooms);
                SetNeighbour(room, MazeRoomNeighbourPosition.Right, rooms);
                SetNeighbour(room, MazeRoomNeighbourPosition.Left, rooms);

                if (room.Position.X == RawMaze.InitialNodeX && room.Position.Y == RawMaze.InitialNodeY)
                {
                    initialRoom = room;
                }
            }

            if (initialRoom == null)
            {
                //TODO: maybe exception
                Debug.LogError("No initial room!");
            }
            var maze = new Maze(rooms.ToList().Select(x => x.Value).ToList(), initialRoom);

            return maze;
        }

        private static void SetNeighbour(MazeRoom room, MazeRoomNeighbourPosition neighbourPositionEnum, Dictionary<MazePosition, MazeRoom> rooms)
        {
            var neighbourPosition = room.Position + neighbourPositionEnum.ToOffset();

            if (rooms.ContainsKey(neighbourPosition))
            {
                var neighbour = rooms[neighbourPosition];
                room.AddNeighbour(neighbourPositionEnum, neighbour);
            }
        }
    }
}