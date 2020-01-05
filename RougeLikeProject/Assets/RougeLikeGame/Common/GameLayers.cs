using System.Linq;
using UnityEngine;

namespace Common
{
    public enum GameLayers
    {
        LandObstacles = 8,
        FlyingPlayer,
        Player,
        Enemy,
        FlyingEnemy,
        Items,
        RoomBoundaries,
        EnemyRay
    }

    public static class GameLayer
    {
        public static int GetMask(params GameLayers[] layers)
        {
            return LayerMask.GetMask(layers.Select(x => x.ToString()).ToArray());
        }
    }
}
