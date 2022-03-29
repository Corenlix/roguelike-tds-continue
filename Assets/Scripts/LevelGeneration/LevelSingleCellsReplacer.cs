namespace LevelGeneration
{
    public class LevelSingleCellsReplacer
    {
        private readonly CellType _replaceCell;
        
        public LevelSingleCellsReplacer(CellType replaceReplaceCell)
        {
            _replaceCell = replaceReplaceCell;
        }

        public void Replace(CellType[,] levelTable)
        {
            bool replaced = false;
            
            int maxX = levelTable.GetLength(0) - 1;
            int maxY = levelTable.GetLength(1) - 1;
            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    if (levelTable[x, y] != CellType.Wall)
                        continue;

                    var upWall = y < maxY ? levelTable[x, y + 1] : CellType.Wall;
                    var downWall = y > 0 ? levelTable[x, y - 1] : CellType.Wall;
                    var rightWall = x < maxX ? levelTable[x + 1, y] : downWall;
                    var leftWall = x > 0 ? levelTable[x - 1, y] : CellType.Wall;

                    if (leftWall != CellType.Wall && rightWall != CellType.Wall ||
                        upWall != CellType.Wall && downWall!= CellType.Wall)
                    {
                        levelTable[x, y] = _replaceCell;
                        replaced = true;
                    }
                }
            }
            
            if(replaced)
                Replace(levelTable);
        }
    }
}
