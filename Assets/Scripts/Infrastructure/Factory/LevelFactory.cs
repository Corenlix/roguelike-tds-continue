using System;
using LevelGeneration;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Factory
{
    public class LevelFactory : ILevelFactory
    {
        public event Action<Level> LevelGenerated;
        private LevelGenerator _levelGenerator;
        private LevelDrawer _levelDrawer;

        public void Generate()
        {
            _levelDrawer?.Clear();
            Object.Destroy(_levelGenerator);
            
            _levelDrawer = new LevelDrawer();
            _levelGenerator = Object.Instantiate(Resources.Load<LevelGenerator>("LevelGenerator"));
            _levelGenerator.LevelCreated += OnLevelGenerate;
            _levelGenerator.Generate();
        }

        private void OnLevelGenerate(Level level)
        {
            _levelGenerator.LevelCreated -= OnLevelGenerate;
            _levelDrawer.DrawLevel(level.LevelTable);
            LevelGenerated?.Invoke(level);
        }
    }
}