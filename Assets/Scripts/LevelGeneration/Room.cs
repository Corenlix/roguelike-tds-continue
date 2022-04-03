using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{
    public class Room
    {
        public int Id;
        public List<Corridor> IntersectCorridors;
        public RectInt Rect;

        public Room(int id, List<Corridor> intersectCorridors, RectInt rect)
        {
            Id = id;
            IntersectCorridors = intersectCorridors;
            Rect = rect;
        }
    }
}