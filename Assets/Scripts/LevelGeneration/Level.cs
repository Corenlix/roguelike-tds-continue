using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace LevelGeneration
{ 
    public class Level
    {
        public CellType[,] LevelTable;
        public Pathfinder Pathfinder;
        private List<Vector2> _floorPoints;

        public Level(CellType[,] levelTable)
        {
            LevelTable = levelTable;
            Pathfinder = new Pathfinder(this);
        }

        public void SetWallCell(Vector2 position)
        {
            LevelTable[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)] = CellType.Wall;
            Pathfinder.UpdateNode(position, true);
            _floorPoints.RemoveAll(x => x == position);
        }
        
        public void SetFloorCell(Vector2 position)
        {
            LevelTable[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)] = CellType.Floor;
            if (LevelTable[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)] == CellType.Wall)
            {
                _floorPoints.Add(new Vector2(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)));
                Pathfinder.UpdateNode(position, false);
            }
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
