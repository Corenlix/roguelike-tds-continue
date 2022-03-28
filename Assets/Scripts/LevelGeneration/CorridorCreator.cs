using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{
    public class CorridorCreator
    {
        private readonly int _corridorThickness;
        
        public CorridorCreator(int corridorThickness)
        {
            _corridorThickness = corridorThickness;
        }

        public Corridor Create(Dungeon first, Dungeon second)
        {
            Corridor bestCorridor = null;
            var bestCorridorSize = int.MaxValue;

            var firstRooms = first.Rooms;
            var secondRooms = second.Rooms;

            foreach (var firstRoom in firstRooms)
            {
                foreach (var secondRoom in secondRooms)
                {
                    var corridor = GetCorridorBetweenRooms(firstRoom.Rect, secondRoom.Rect);

                    int corridorSize = corridor.GetEstimatedSize();
                    if(corridorSize < bestCorridorSize) 
                    {
                        bestCorridorSize = corridorSize;
                        bestCorridor = corridor;
                    }
                }
            }

            return bestCorridor;
        }
        
        private Corridor GetCorridorBetweenRooms(RectInt firstRoom, RectInt secondRoom) 
        {
            var firstPoint = new Vector2Int(Random.Range(firstRoom.x, firstRoom.xMax), Random.Range(firstRoom.y, firstRoom.yMax));
            var secondPoint = new Vector2Int(Random.Range(secondRoom.x, secondRoom.xMax), Random.Range(secondRoom.y, secondRoom.yMax));

            var horizontalRect = new RectInt(); 

            horizontalRect.SetMinMax(new Vector2Int(Mathf.Min(firstPoint.x, secondPoint.x), secondPoint.y),
                new Vector2Int(Mathf.Max(firstPoint.x, secondPoint.x), secondPoint.y + _corridorThickness - 1));

            var verticalRect = new RectInt();
            verticalRect.SetMinMax(new Vector2Int(firstPoint.x, Mathf.Min(firstPoint.y, secondPoint.y)),
                new Vector2Int(firstPoint.x + _corridorThickness - 1, Mathf.Max(firstPoint.y + 1, secondPoint.y + 1)));

            return new Corridor( new List<RectInt> { horizontalRect, verticalRect } );
        }
    }
}
