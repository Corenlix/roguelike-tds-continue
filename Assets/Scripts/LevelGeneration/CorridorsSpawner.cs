using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{
    public class CorridorsSpawner
    {
        private readonly CorridorGameObject _corridorGameObjectPrefab;
        private readonly Transform _parent;

        private List<CorridorGameObject> _corridors;

        public CorridorsSpawner(CorridorGameObject corridorGameObjectPrefab, Transform parent)
        {
            _corridorGameObjectPrefab = corridorGameObjectPrefab;
            _parent = parent;
        }
    
        public List<CorridorGameObject> Spawn(List<ExactEdge<RoomGameObject>> edges)
        {
            _corridors = new List<CorridorGameObject>();
            foreach (var edge in edges)
            {
                var firstRoom = (RoomGameObject) edge.P;
                var secondRoom = (RoomGameObject) edge.Q;
            
                if (CheckYMidpoint(firstRoom, secondRoom))
                {
                    SpawnHorizontalCorridor(firstRoom.transform.localPosition.x, secondRoom.transform.localPosition.x, secondRoom.transform.localPosition.y);
                }
                else if (CheckYMidpoint(secondRoom, firstRoom))
                {
                    SpawnHorizontalCorridor(firstRoom.transform.localPosition.x, secondRoom.transform.localPosition.x, firstRoom.transform.localPosition.y);
                }
                else if (CheckXMidpoint(firstRoom, secondRoom))
                {
                    SpawnVerticalCorridor(firstRoom.transform.localPosition.y, secondRoom.transform.localPosition.y, secondRoom.transform.localPosition.x);
                }
                else if (CheckXMidpoint(secondRoom, firstRoom))
                {
                    SpawnVerticalCorridor(secondRoom.transform.localPosition.y, firstRoom.transform.localPosition.y, firstRoom.transform.localPosition.x);
                }
                else
                {
                    SpawnHorizontalCorridor(firstRoom.transform.localPosition.x, secondRoom.transform.localPosition.x, firstRoom.transform.localPosition.y);
                    SpawnVerticalCorridor(secondRoom.transform.localPosition.y, firstRoom.transform.localPosition.y, secondRoom.transform.localPosition.x);
                }
            }
            return _corridors;
        }
    
        private bool CheckXMidpoint(RoomGameObject x, RoomGameObject y)
        {
            float leftSide = x.transform.localPosition.x - x.transform.localScale.x / 2;
            float rightSide = x.transform.localPosition.x + x.transform.localScale.x / 2;
            return y.transform.localPosition.x > leftSide && y.transform.localPosition.x < rightSide;
        }

        private bool CheckYMidpoint(RoomGameObject x, RoomGameObject y)
        {
            float leftSide = x.transform.localPosition.y - x.transform.localScale.y / 2;
            float rightSide = x.transform.localPosition.y + x.transform.localScale.y / 2;
            return y.transform.localPosition.y > leftSide && y.transform.localPosition.y < rightSide;
        }

        private void SpawnHorizontalCorridor(float fromX, float toX, float y)
        {
            SpawnCorridor(_parent.transform.position + new Vector3((fromX + toX) / 2f, y), new Vector3(Mathf.Abs(fromX - toX) + 1, 1));
        }

        private void SpawnVerticalCorridor(float fromY, float toY, float x)
        {
            SpawnCorridor(_parent.transform.position + new Vector3(x, (fromY + toY) / 2f), new Vector3(1, Mathf.Abs(fromY - toY) + 1));
        }

        private void SpawnCorridor(Vector3 position, Vector3 scale)
        {
            var corridor = Object.Instantiate(_corridorGameObjectPrefab, position, Quaternion.identity, _parent);
            corridor.transform.localScale = scale;
            _corridors.Add(corridor);
        }
    }
}
