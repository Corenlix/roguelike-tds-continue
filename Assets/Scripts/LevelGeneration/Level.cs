using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{ 
    public class Level
    {
        public CellType[,] LevelTable;

        public Level(CellType[,] levelTable)
        {
            LevelTable = levelTable;
        }
        
        public List<Vector2> GetFloorPoints()
        {
            var points = new List<Vector2>();
            for (int i = 0; i < LevelTable.GetLength(0); i++)
            {
                for (int j = 0; j < LevelTable.GetLength(1); j++)
                {
                    if(LevelTable[i,j] == CellType.Floor)
                        points.Add(new Vector2(i,j));
                }
            }

            return points;
        }
    }
}
