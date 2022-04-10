using System.Collections.Generic;

namespace LevelGeneration
{ 
    public class Level
    {
        public CellType[,] LevelTable;
        public List<Room> SecondaryRooms;
        public List<Room> MainRooms;
        public  List<Corridor> Corridors;
        
        public Level(CellType[,] levelTable,  List<Room> mainRooms, List<Room> secondaryRooms, List<Corridor> corridors)
        {
            LevelTable = levelTable;
            SecondaryRooms = secondaryRooms;
            Corridors = corridors;
            MainRooms = mainRooms;
        }
    }
}
