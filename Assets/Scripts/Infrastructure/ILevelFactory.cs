using System;
using LevelGeneration;

namespace Infrastructure
{
    public interface ILevelFactory
    {
        event Action<Level> LevelGenerated;
        void Generate();
    }
}