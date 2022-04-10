using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelGeneration
{
    public class LevelDataGenerator
    {
        private List<Room> _allRoomsData;
        private List<Room> _mainRoomsData;
        private List<Room> _secondaryRoomsData;
        private List<Corridor> _corridorsData;
        private CellType[,] _table;
        private int _xOffset;
        private int _yOffset;

        public Level Generate(List<RoomGameObject> rooms, List<RoomGameObject> mainRooms, List<CorridorGameObject> corridors)
        {
            GenerateRects(rooms, mainRooms, corridors);
            GenerateTable();
            GenerateRects(rooms, mainRooms, corridors);
            
            return new Level(_table, _mainRoomsData, _secondaryRoomsData, _corridorsData);
        }

        private void GenerateRects(List<RoomGameObject> rooms, List<RoomGameObject> mainRooms, List<CorridorGameObject> corridors)
        {
            var secondaryRooms = rooms.Except(mainRooms).ToList();
            _secondaryRoomsData = new List<Room>();
            secondaryRooms.ForEach(x => _secondaryRoomsData.Add(x.GetData(_xOffset, _yOffset)));
            _mainRoomsData = new List<Room>();
            mainRooms.ForEach(x => _mainRoomsData.Add(x.GetData(_xOffset, _yOffset)));
            _corridorsData = new List<Corridor>();
            corridors.ForEach(x => _corridorsData.Add(x.GetData(_xOffset, _yOffset)));
        }

        private void GenerateTable()
        {
            InitCells();
            FillBackground();
            FillForeground();
        }

        private void InitCells()
        {
            _allRoomsData = new List<Room>();
            _allRoomsData.AddRange(_mainRoomsData);
            _allRoomsData.AddRange(_secondaryRoomsData);
            int minX = _allRoomsData.Min(x => x.Rect.xMin) - 10; 
            int maxX = _allRoomsData.Max(x => x.Rect.xMax) + 10; 
            int minY = _allRoomsData.Min(x => x.Rect.yMin) - 10; 
            int maxY = _allRoomsData.Max(x => x.Rect.yMax) + 10; 

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
            _corridorsData.ForEach(x=>DrawRect(x.Rect));
            _allRoomsData.ForEach(x=>DrawRect(x.Rect));
        }
    
        private void DrawRect(RectInt rect)
        {
            foreach (var position in rect.allPositionsWithin)
            {
                _table[position.x + _xOffset, position.y + _yOffset] = CellType.Floor;
            }
        }
    }
}
