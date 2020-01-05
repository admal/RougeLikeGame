
using UnityEngine;

namespace MazeGeneration
{
    public class MazePosition
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MazePosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static MazePosition operator +(MazePosition pos1, MazePosition pos2)
        {
            var pos3 = new MazePosition(pos1.X + pos2.X, pos1.Y + pos2.Y);
            return pos3;
        }

        public static MazePosition operator +(MazePosition pos1, Vector2 offset)
        {
            var pos3 = new MazePosition(pos1.X + (int)offset.x, pos1.Y + (int)offset.y);
            return pos3;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj is MazePosition == false)
            {
                return false;
            }

            var position = obj as MazePosition;
            return position.X == this.X && position.Y == this.Y;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + X.GetHashCode();
            hash = hash * 23 + Y.GetHashCode();
            return hash;
        }
    }
}