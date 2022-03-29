namespace LevelGeneration
{ 
    public class Level
    {
        public CellType[,] LevelCells { get; }
        public Dungeon Dungeon { get; }
        
        public Level(CellType[,] levelCells, Dungeon dungeon)
        {
            LevelCells = levelCells;
            Dungeon = dungeon;
        }
    }
}
