using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        public event Action<Level> LevelCreated;
        
        [SerializeField] private RoomGameObject _roomPrefab;
        [SerializeField] private CorridorGameObject corridorPrefab;
        [SerializeField] private int _mainRoomsCount;
        [SerializeField] private int _roomsCount = 100;
        [SerializeField] private int _minRoomSize, _maxRoomSize;
        [Range(0, 100)] [SerializeField] private float _extraConnections;
        [SerializeField] private int _extraWallsSize = 1;
        private bool _snapRooms = true;
        private List<RoomGameObject> _rooms;
        private List<RoomGameObject> _mainRooms;
        private List<CorridorGameObject> _corridors = new List<CorridorGameObject>();
        private LevelGraph<RoomGameObject> _levelGraph;
    
        public void Generate()
        {
            Time.timeScale = 100;
            _rooms = new RoomsSpawner(_roomPrefab, transform, _minRoomSize, _maxRoomSize, _minRoomSize, _maxRoomSize).Spawn(_roomsCount);
            _mainRooms = new MainRoomSelector(_mainRoomsCount).Select(_rooms);
            _mainRooms.ForEach(x=>x.GetComponent<SpriteRenderer>().color = Color.red);
            ScaleRoomsForExtraWallsSize();
            Invoke(nameof(SpawnCorridors), 100f);
        }

        private void ScaleRoomsForExtraWallsSize()
        {
            foreach (var chamber in _rooms)
            {
                chamber.transform.localScale = new Vector3(chamber.transform.localScale.x + _extraWallsSize * 2,
                    chamber.transform.localScale.y + _extraWallsSize * 2);
            }
        }

        private void UnscaleRoomsForExtraWallsSize()
        {
            foreach (var chamber in _rooms)
            {
                chamber.transform.localScale = new Vector3(chamber.transform.localScale.x - _extraWallsSize * 2,
                    chamber.transform.localScale.y - _extraWallsSize * 2);
            }
        }
    
        private void FixedUpdate()
        {
            if(_snapRooms)
                SnapRooms();
        }

        private void SnapRooms()
        {
            foreach (var room in _rooms)
            {
                var roomPosition = room.transform.localPosition;
                room.transform.localPosition = new Vector2(Mathf.Floor(roomPosition.x), Mathf.Floor(roomPosition.y));
            }
        }
    
        private void SpawnCorridors()
        {
            Time.timeScale = 1;
            _snapRooms = false;
            UnscaleRoomsForExtraWallsSize();
            _levelGraph = new LevelGraph<RoomGameObject>(_mainRooms, _extraConnections);
            _levelGraph.Edges.ForEach(x => Debug.DrawLine(new Vector3((float)x.P.X, (float)x.P.Y), new Vector3((float)x.Q.X, (float)x.Q.Y), Color.green, 50000000));
            _corridors = new CorridorsSpawner(corridorPrefab, transform).Spawn(_levelGraph.Edges);
            _mainRooms = new PointsSorter<RoomGameObject>().Sort(_levelGraph.Edges);
            Invoke(nameof(FinalGenerate), 0.5f);
        }
        
        private void FinalGenerate()
        {
            ClearRooms();
            var level = new LevelDataGenerator().Generate(_rooms,   _mainRooms, _corridors);
            new LevelScaler().Scale(level, 2);

            LevelCreated?.Invoke(level);
        }

        private void ClearRooms()
        {
            List<RoomGameObject> roomsToDestroy = new List<RoomGameObject>();
            foreach (var chamber in _rooms.Where(chamber => chamber.NeedDestroy))
            {
                Destroy(chamber.gameObject);
                roomsToDestroy.Add(chamber);
            }
            foreach (var chamber in roomsToDestroy)
                _rooms.Remove(chamber);
        }
    }
}
