using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{
    public class CorridorGameObject : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out RoomGameObject room))
            {
                room.IntersectCorridors.Add(this);
            }
        }

        public Corridor GetData(int xOffset = 0, int yOffset = 0)
        {
            var rect = new RectInt();
            rect.SetMinMax(new Vector2Int(Mathf.FloorToInt(transform.localPosition.x - transform.localScale.x) + xOffset, Mathf.FloorToInt(transform.localPosition.y - transform.localScale.y) + yOffset), 
                new Vector2Int(Mathf.FloorToInt(transform.localPosition.x + transform.localScale.x) + xOffset, Mathf.FloorToInt(transform.localPosition.y + transform.localScale.y) + yOffset));
            return new Corridor(rect);
        }
    }
}
