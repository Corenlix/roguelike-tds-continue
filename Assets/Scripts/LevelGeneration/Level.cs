namespace LevelGeneration
{ 
    public class Level
    {
        public CellType[,] LevelTable { get; }
        public Dungeon Dungeon { get; }
        
        public Level(CellType[,] levelTable, Dungeon dungeon)
        {
            LevelTable = levelTable;
            Dungeon = dungeon;
        }
    }
}
