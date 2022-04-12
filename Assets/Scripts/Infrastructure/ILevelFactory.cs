using System;
using LevelGeneration;

namespace Infrastructure
{
    public interface ILevelFactory : IService
    {
        event Action<Level> LevelGenerated;
        void Generate();
    }
}