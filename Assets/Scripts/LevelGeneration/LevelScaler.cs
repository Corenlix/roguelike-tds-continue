using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace LevelGeneration
{
    public class LevelScaler
    {
        public static void Scale(Level level, int modifier)
        {
            var oldTable = level.LevelTable;
            var table = new CellType[oldTable.GetLength(0) * modifier, oldTable.GetLength(1) * modifier];
        
            for(int i = 0; i < oldTable.GetLength(0); i++)
            for(int j = 0; j < oldTable.GetLength(1); j++)
            for (int dI = 0; dI < modifier; dI++)
            for (int dJ = 0; dJ < modifier; dJ++)
            {
                table[i*modifier + dI, j*modifier + dJ] = oldTable[i, j];
            }

            level.LevelTable = table;
        }


    }
}
