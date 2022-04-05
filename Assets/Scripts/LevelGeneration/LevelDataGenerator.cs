using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelGeneration
{
    public class LevelDataGenerator
    {
        private List<RoomGameObject> _rooms;
        private List<CorridorGameObject> _corridors;
        private CellType[,] _table;
        private int _xOffset;
        private int _yOffset;

        public Level Generate(List<RoomGameObject> rooms, List<RoomGameObject> mainRooms, List<CorridorGameObject> corridors)
        {
            _rooms = rooms;
            _corridors = corridors;
        
            InitCells();
            FillBackground();
            FillForeground();
            
            var roomsData = new List<Room>();
            _rooms.ForEach(x=>roomsData.Add(x.GetData(_xOffset, _yOffset)));
            var mainRoomsData = new List<Room>();
            mainRooms.ForEach(x=>mainRoomsData.Add(x.GetData(_xOffset, _yOffset)));
            var corridorsData = new List<Corridor>();
            _corridors.ForEach(x=>corridorsData.Add(x.GetData(_xOffset, _yOffset)));
            
            return new Level(_table, mainRoomsData, roomsData, corridorsData);
        }

        private void InitCells()
        {
            int minX = (int)_rooms.Min(x => x.transform.localPosition.x - x.transform.localScale.x/2) - 10; 
            int maxX = (int)_rooms.Max(x => x.transform.localPosition.x + x.transform.localScale.x/2) + 10; 
            int minY = (int)_rooms.Min(x => x.transform.localPosition.y - x.transform.localScale.y/2) - 10; 
            int maxY = (int)_rooms.Max(x => x.transform.localPosition.y + x.transform.localScale.y/2) + 10;

            _xOffset = -minX;
            _yOffset = -minY;
        
            _table = new CellType[maxX - minX, maxY - minY];
        }

        private void FillBackground()
        {
            for(int i = 0; i < _table.GetLength(0); i++)
            for(int j = 0; j < _table.GetLength(1); j++)
                _table[i,j] = CellType.Wall;
        }
    
        private void FillForeground()
        {
            _corridors.ForEach(x=>DrawTransform(x.transform));
            _rooms.ForEach(x=>DrawTransform(x.transform));
        }
    
        private void DrawTransform(Transform transform)
        {
            for (int x = Mathf.CeilToInt(transform.localPosition.x - transform.localScale.x/2f);
                 x < Mathf.CeilToInt(transform.localPosition.x + transform.localScale.x/2f);
                 x++)
            {
                for (int y = Mathf.CeilToInt(transform.localPosition.y - transform.localScale.y/2f);
                     y < Mathf.CeilToInt(transform.localPosition.y + transform.localScale.y/2f);
                     y++)
                {
                    _table[x + _xOffset, y + _yOffset] = CellType.Floor;
                }
            }
        }
    }
}
