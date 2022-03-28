namespace LevelGeneration
{ 
    public class Level
    {
        public CellType[,] LevelCells { get; }
        
        public Level(CellType[,] levelCells)
        {
            LevelCells = levelCells;
        }
    }
}
