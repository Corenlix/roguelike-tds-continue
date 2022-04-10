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
            rect.SetMinMax(new Vector2Int(Mathf.CeilToInt(transform.localPosition.x - transform.localScale.x / 2) + xOffset, Mathf.CeilToInt(transform.localPosition.y - transform.localScale.y / 2) + yOffset), 
                new Vector2Int(Mathf.CeilToInt(transform.localPosition.x + transform.localScale.x / 2) + xOffset, Mathf.CeilToInt(transform.localPosition.y + transform.localScale.y / 2) + yOffset));
            return new Corridor(rect);
        }
    }
}
