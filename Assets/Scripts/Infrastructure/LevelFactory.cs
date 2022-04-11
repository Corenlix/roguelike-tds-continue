using System;
using LevelGeneration;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure
{
    public class LevelFactory : ILevelFactory
    {
        public event Action LevelGenerated;
        public Level Level { get; private set; }

        private LevelGenerator _levelGenerator;
        private LevelDrawer _levelDrawer;

        public LevelFactory()
        {
            _levelDrawer = new LevelDrawer();
            GenerateLevel();
        }
        
        private void GenerateLevel()
        {
            _levelGenerator = Object.Instantiate(Resources.Load<LevelGenerator>("LevelGenerator"));
            _levelGenerator.LevelCreated += OnLevelGenerate;
            _levelGenerator.Generate();
        }

        private void OnLevelGenerate(Level level)
        {
            Level = level;
            _levelGenerator.LevelCreated -= OnLevelGenerate;
            _levelDrawer.DrawLevel(Level.LevelTable);
            LevelGenerated?.Invoke();
        }
    }
}