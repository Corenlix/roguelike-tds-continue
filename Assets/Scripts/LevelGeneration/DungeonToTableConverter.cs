using UnityEngine;

namespace LevelGeneration
{
    public class DungeonToTableConverter
    {
        private CellType _roomCell;
        private CellType _wallCell;
        private CellType _corridorCell;

        private Dungeon _dungeon;
        private CellType[,] _tableCells;
        
        public DungeonToTableConverter(CellType roomCell, CellType wallCell, CellType corridorCell)
        {
            _roomCell = roomCell;
            _wallCell = wallCell;
            _corridorCell = corridorCell;
        }

        public CellType[,] Convert(Dungeon dungeon)
        {
            _dungeon = dungeon;
            InitTable();
            FillCorridors();
            FillRooms();
            
            return _tableCells;
        }

        private void InitTable()
        {
            int dungeonWidth = _dungeon.Rect.width;
            int dungeonHeight = _dungeon.Rect.height;
            _tableCells = new CellType[dungeonWidth, dungeonHeight];
            for(int i = 0; i < dungeonWidth; i++) 
            {
                for(int j = 0; j < dungeonHeight; j++) 
                {
                    _tableCells[i, j] = _wallCell;
                }
            }
        }
        
        private void FillCorridors()
        {
            var corridors = _dungeon.Corridors;
            foreach (var corridor in corridors)
            {
                foreach (var rect in corridor.Rects)
                {
                    FillRectToTable(rect, _corridorCell);
                }
            }
        }

        private void FillRooms()
        {
            var rooms = _dungeon.Rooms;
            foreach(var room in rooms) 
            {
                FillRectToTable(room.Rect, _roomCell);
            }
        }
        
        private void FillRectToTable(RectInt roomRect, CellType cellsType) 
        {
            for(int i = roomRect.x; i <= roomRect.xMax; i++) 
            {
                for (int j = roomRect.y; j <= roomRect.yMax; j++)
                {
                    if (i < _tableCells.GetLength(0) && j < _tableCells.GetLength(1) && i > 0 && j > 0)
                        _tableCells[i, j] = cellsType;
                }
            }
        }
    }
}
