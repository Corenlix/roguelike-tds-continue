using UnityEngine;

namespace LevelGeneration
{
    public class Room
    {
        public RectInt Rect { get; }

        public Room(RectInt rect)
        {
            Rect = rect;
        }
    }
}
