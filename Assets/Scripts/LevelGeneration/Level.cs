using System.Collections.Generic;

namespace LevelGeneration
{ 
    public class Level
    {
        public CellType[,] LevelTable;
        public List<Room> Rooms;
        public List<Room> MainRooms;
        public  List<Corridor> Corridors;
        
        public Level(CellType[,] levelTable,  List<Room> mainRooms, List<Room> rooms, List<Corridor> corridors)
        {
            LevelTable = levelTable;
            Rooms = rooms;
            Corridors = corridors;
            MainRooms = mainRooms;
        }
    }
}
