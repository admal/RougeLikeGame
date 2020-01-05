namespace MazeGeneration
{
    public enum MazeRoomType
    {
        Initial,
        Normal,
        Boss,
        Item,
        //Secret???
    }
    public class MazeNode
    {
        public MazeNode(MazeRoomType type)
        {
            RoomType = type;
        }
        public bool Visited { get; set; }
        public int Depth { get; set; }
        public MazeRoomType RoomType { get; set; }
    }
}