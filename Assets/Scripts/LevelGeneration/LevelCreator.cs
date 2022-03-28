using UnityEngine;

namespace LevelGeneration
{
    
    [CreateAssetMenu(menuName = "LevelCreator", order = 1)]
    internal class LevelCreator : ScriptableObject
    {
        [SerializeField] private int _levelSize = 64;
        [Range(0,10)][SerializeField] private int _splitTimes = 4;
        
        [Range(0, 5000)][SerializeField] private int _walkerRoomPart = 60;
        [Range(0, 100)][SerializeField] private int _walkerRotate90Chance = 60;
        [Range(0, 100)][SerializeField] private int _walkerRotate180Chance = 5;
        
        [Range(10, 100)][SerializeField] private int _minRoomSizePercent = 20;
        [Range(10, 100)][SerializeField] private int _maxRoomSizePercent = 80;
        [Range(1,5)][SerializeField] private int _corridorsThickness = 2;
        
        private CellType[,] _levelTable;
        
        public Level CreateLevel()
        {
            var rootDungeon = new Dungeon(new RectInt(0, 0, _levelSize - 1, _levelSize - 1), _splitTimes, new RoomCreator(_minRoomSizePercent, _maxRoomSizePercent), new CorridorCreator(_corridorsThickness));
            
            var dungeonToTableConverter =
                new DungeonToTableConverter(CellType.RoomFloor, CellType.Wall, CellType.CorridorFloor);
            _levelTable = dungeonToTableConverter.Convert(rootDungeon);
            
            var walker = new Walker(CellType.RoomFloor, _walkerRotate90Chance, _walkerRotate180Chance);
            _levelTable = walker.Walk(rootDungeon, _levelTable, _walkerRoomPart / 100f);

            var singeCellReplacer = new LevelSingleCellsReplacer(CellType.RoomFloor);
            _levelTable = singeCellReplacer.Replace(_levelTable);
            
            return new Level(_levelTable);
        }
    }
    
    public enum CellType
    {
        Wall,
        RoomFloor,
        CorridorFloor,
    }
}
