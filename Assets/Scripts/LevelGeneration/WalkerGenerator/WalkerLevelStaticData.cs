using UnityEngine;

namespace LevelGeneration.WalkerGenerator
{
    [CreateAssetMenu(fileName = "Level", menuName = "Static Data/Level")]
    public class WalkerLevelStaticData : LevelStaticData
    {
        public Vector2Int LevelSize;
        public int Steps;
        [Range(2,6)]
        public int Scale;
        [Range(0,100)]
        public int Rotate90Chance;
        [Range(0,100)]
        public int Rotate180Chance;
        
        public override Level Generate()
        {
            return new WalkerLevelGenerator(this).Generate();
        }
    }
}