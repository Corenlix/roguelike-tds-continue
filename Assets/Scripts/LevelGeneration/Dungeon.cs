using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using LevelGeneration;
using UnityEngine;

namespace LevelGeneration
{
    public class Dungeon
    {
        public RectInt Rect { get; }
        public List<Corridor> Corridors => _corridors;
        public List<Room> Rooms => _rooms;
        
        private readonly List<Corridor> _corridors = new List<Corridor>();
        private readonly List<Room> _rooms = new List<Room>();

        public Dungeon(RectInt rect, int splitsCount, RoomCreator roomCreator, CorridorCreator corridorCreator)
        {
            Rect = rect;
            
            bool isBoundary = splitsCount == 0;
            if (isBoundary)
            {
                _rooms.Add(roomCreator.Create(rect));
                return;
            }
            
            var newRects = RectSplitter.SplitRect(rect);
            int childrenSplitsCount = splitsCount - 1;
            
            var firstChild = new Dungeon(newRects[0], childrenSplitsCount, roomCreator, corridorCreator);
            var secondChild = new Dungeon(newRects[1], childrenSplitsCount, roomCreator, corridorCreator);
            
            var corridorBetweenChildren = corridorCreator.Create(firstChild, secondChild);
            _corridors.Add(corridorBetweenChildren);
            
            IncludeSubFragment(firstChild);
            IncludeSubFragment(secondChild);
        }

        private void IncludeSubFragment(Dungeon sub)
        {
            _rooms.AddRange(sub._rooms);
            _corridors.AddRange(sub.Corridors);
        }
    }
}
