using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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
            var bestCorridorSize = Int32.MaxValue;

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
            var firstRoomPoint = new Vector2Int(Random.Range(firstRoom.x, firstRoom.xMax), Random.Range(firstRoom.y, firstRoom.yMax));
            var secondRoomPoint = new Vector2Int(Random.Range(secondRoom.x, secondRoom.xMax), Random.Range(secondRoom.y, secondRoom.yMax));
            
            var horizontalRect = new RectInt();
            int horizontalRectX = Mathf.Min(firstRoomPoint.x, secondRoomPoint.x);
            int horizontalRectY = secondRoomPoint.y;
            int horizontalRectXMax = Mathf.Max(firstRoomPoint.x, secondRoomPoint.x);
            int horizontalRectYMax = secondRoomPoint.y + _corridorThickness - 1;
            
            horizontalRect.SetMinMax(new Vector2Int(horizontalRectX, horizontalRectY),
                new Vector2Int(horizontalRectXMax, horizontalRectYMax));

            var verticalRect = new RectInt();
            int verticaRectX = firstRoomPoint.x;
            int verticaRectY = Mathf.Min(firstRoomPoint.y, secondRoomPoint.y);
            int verticaRectXMax = firstRoomPoint.x + _corridorThickness - 1;
            int verticaRectYMax = Mathf.Max(firstRoomPoint.y, secondRoomPoint.y);
            
            verticalRect.SetMinMax(new Vector2Int(verticaRectX, verticaRectY),
                new Vector2Int(verticaRectXMax, verticaRectYMax));

            return new Corridor( new List<RectInt> { horizontalRect, verticalRect } );
        }
    }
}
