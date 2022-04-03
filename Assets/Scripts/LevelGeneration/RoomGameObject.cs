using System.Collections.Generic;
using DelaunatorSharp;
using UnityEngine;

namespace LevelGeneration
{
    public class RoomGameObject : MonoBehaviour, IPoint
    {
        public bool NeedDestroy => IntersectCorridors.Count == 0;
        public int Id { get; private set; }
        public List<CorridorGameObject> IntersectCorridors { get; } = new List<CorridorGameObject>();
        public double X
        {
            get => transform.localPosition.x;
            set
            {
            
            }
        }
        public double Y
        {
            get => transform.localPosition.y;
            set
            {
            
            }
        }
    
        public static RoomGameObject Create(RoomGameObject prefab, Vector2 size, Vector3 spawnPoint, Transform parent, int id)
        {
            var createdChamber = Instantiate(prefab, spawnPoint + parent.transform.position, Quaternion.identity, parent);
            createdChamber.transform.localScale = size;
            createdChamber.Id = id;
            return createdChamber;
        }

        public Room GetData(int xOffset = 0, int yOffset = 0)
        {
            var rect = new RectInt();
            rect.SetMinMax(new Vector2Int(Mathf.FloorToInt(transform.localPosition.x - transform.localScale.x) + xOffset, Mathf.FloorToInt(transform.localPosition.y - transform.localScale.y) + yOffset), 
                new Vector2Int(Mathf.FloorToInt(transform.localPosition.x + transform.localScale.x) + xOffset, Mathf.FloorToInt(transform.localPosition.y + transform.localScale.y) + yOffset));
            var intersectCorridors = new List<Corridor>();
            IntersectCorridors.ForEach(x=>intersectCorridors.Add(x.GetData()));
            return new Room(Id, intersectCorridors, rect);
        }
    }
}
