using System.Collections;
using System.Collections.Generic;
using LevelGeneration;
using UnityEngine;

namespace LevelGeneration
{
    public class RoomCreator
    {
        private readonly float _minRelativeRoomSize;
        private readonly float _maxRelativeRoomSize;

        public RoomCreator(float minRoomSizePercent, float maxRoomSizePercent)
        {
            _minRelativeRoomSize = minRoomSizePercent / 100f;
            _maxRelativeRoomSize = maxRoomSizePercent / 100f;
        }

        public Room Create(RectInt rect)
        {
            var roomSize = GetRoomSize(rect.size);
            
            var x = rect.x + Random.Range(0, rect.width - roomSize.x);
            var y = rect.y + Random.Range(0, rect.height - roomSize.y);

            var roomRect = new RectInt(x, y, roomSize.x, roomSize.y);
            return new Room(roomRect);
        }
        
        private Vector2Int GetRoomSize(Vector2Int size)
        {
            var roomSizeModifier = Random.Range(_minRelativeRoomSize, _maxRelativeRoomSize);
            return new Vector2Int((int)(size.x * roomSizeModifier), (int)(size.y * roomSizeModifier));
        }
    }
}