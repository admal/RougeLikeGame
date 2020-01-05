using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public enum GameTag
    {
        Player,
        Floor,
        Door,
        Enemy
    }

    public static class GameTagsExtensions
    {
        public static GameTag ToGameTag(this string stringTag)
        {
            if(Enum.TryParse<GameTag>(stringTag, out var result))
            {
                return result;
            }
            throw new ArgumentException("String tag is not a game tag!");

        }
        public static bool HasTag(this GameObject gameObject, GameTag tag)
        {
            return gameObject.tag == tag.ToString();
        }

        public static bool IsPlayer(this GameObject gameObject)
        {
            return gameObject.HasTag(GameTag.Player);
        }

        //TMP
        public static string ListToString<T>(this List<T> @this)
        {
            var str = "[";
            foreach(var elem in @this)
            {
                str += $"{elem.ToString()}, ";
            }
            str += "]";
            return str;
        }
    }
}