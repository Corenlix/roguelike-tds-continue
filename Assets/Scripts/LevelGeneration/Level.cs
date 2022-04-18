using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{ 
    public class Level
    {
        public CellType[,] LevelTable;
        private List<Vector2> _floorPoints;

        public Level(CellType[,] levelTable)
        {
            LevelTable = levelTable;
        }
        
        public List<Vector2> GetFloorPoints()
        {
            if (_floorPoints != null)
                return _floorPoints;
            
            _floorPoints = new List<Vector2>();
            for (int i = 0; i < LevelTable.GetLength(0); i++)
            {
                for (int j = 0; j < LevelTable.GetLength(1); j++)
                {
                    if(LevelTable[i,j] == CellType.Floor)
                        _floorPoints.Add(new Vector2(i,j));
                }
            }

            return _floorPoints;
        }
    }
}
