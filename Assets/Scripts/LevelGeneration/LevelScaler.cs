using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace LevelGeneration
{
    public class LevelScaler
    {
        private Level _level;
        private int _modifier;
    
        public void Scale(Level level, int modifier)
        {
            _level = level;
            _modifier = modifier;
            ScaleTable();
            ScaleRooms();
            ScaleCorridors();
        }

        private void ScaleTable()
        {
            var oldTable = _level.LevelTable;
            var table = new CellType[oldTable.GetLength(0) * _modifier, oldTable.GetLength(1) * _modifier];
        
            for(int i = 0; i < oldTable.GetLength(0); i++)
            for(int j = 0; j < oldTable.GetLength(1); j++)
            for (int dI = 0; dI < _modifier; dI++)
            for (int dJ = 0; dJ < _modifier; dJ++)
            {
                table[i*_modifier + dI, j*_modifier + dJ] = oldTable[i, j];
            }

            _level.LevelTable = table;
        }

        private void ScaleRooms()
        {
            foreach (var levelRoom in _level.Rooms)
            {
                levelRoom.Rect = ScaleRect(levelRoom.Rect);
            }
            foreach (var levelRoom in _level.MainRooms)
            {
                levelRoom.Rect = ScaleRect(levelRoom.Rect);
            }
        }

        private void ScaleCorridors()
        {
            foreach (var corridor in _level.Corridors)
            {
                corridor.Rect = ScaleRect(corridor.Rect);
            }
        }

        private RectInt ScaleRect(RectInt rect)
        {
            rect.SetMinMax(new Vector2Int(rect.xMin * _modifier, rect.yMin * _modifier), new Vector2Int(rect.xMax * _modifier, rect.yMax * _modifier));
            return rect;
        }
    }
}
