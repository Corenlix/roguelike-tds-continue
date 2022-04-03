using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{
    public class RoomsSpawner
    {
        private readonly RoomGameObject _prefab;
        private Transform _parent;
        private readonly int _minRoomWidth;
        private readonly int _maxRoomWidth;
        private readonly int _minRoomHeight;
        private readonly int _maxRoomHeight;

        public RoomsSpawner(RoomGameObject prefab, Transform parent, int minRoomWidth, int maxRoomWidth, int minRoomHeight, int maxRoomHeight)
        {
            _prefab = prefab;
            _parent = parent;
            _minRoomWidth = minRoomWidth;
            _maxRoomWidth = maxRoomWidth;
            _minRoomHeight = minRoomHeight;
            _maxRoomHeight = maxRoomHeight;
        }
    
        public List<RoomGameObject> Spawn(int count)
        {
            var rooms = new List<RoomGameObject>();
            for (int i = 0; i < count; i++)
            {
                rooms.Add(CreateRoom(NormalRandom.Range(_minRoomWidth, _maxRoomWidth), NormalRandom.Range(_minRoomHeight, _maxRoomHeight), i));
            }

            return rooms;
        }

        private RoomGameObject CreateRoom(int width, int height, int id) => RoomGameObject.Create(_prefab, new Vector2(width, height), GetRandomPointInCircle(10), _parent, id);

        private Vector2 GetRandomPointInCircle(float radius)
        {
        
            float angle = Random.Range(0, Mathf.PI * 2);
            float r = Random.Range(-radius, radius);
            return new Vector2(r * Mathf.Cos(angle), r * Mathf.Sin(angle));
        }
    }
}
