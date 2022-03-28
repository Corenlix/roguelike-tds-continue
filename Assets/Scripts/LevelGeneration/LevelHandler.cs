using UnityEngine;

namespace LevelGeneration
{
    public class LevelHandler : MonoBehaviour
    {
        [SerializeField] private LevelCreator levelCreator;
        [SerializeField] private LevelDrawer levelDrawer;
    
        [ContextMenu("Create Level")]
        private Level SpawnLevel()
        {
            var level = levelCreator.CreateLevel();
            levelDrawer.DrawLevel(level.LevelCells);
            return level;
        }
        private void Awake()
        {
            SpawnLevel();
        }
    }
}