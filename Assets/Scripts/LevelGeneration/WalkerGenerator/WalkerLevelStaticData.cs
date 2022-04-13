using UnityEngine;

namespace LevelGeneration.WalkerGenerator
{
    [CreateAssetMenu(fileName = "Level", menuName = "Static Data/Level")]
    public class WalkerLevelStaticData : LevelStaticData
    {
        public Vector2Int LevelSize;
        public int Steps;
        public int Rotate90Chance;
        public int Rotate180Chance;
        
        public override Level Generate()
        {
            return new WalkerLevelGenerator(this).Generate();
        }
    }
}