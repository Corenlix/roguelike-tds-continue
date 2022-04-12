using System;
using LevelGeneration;

namespace Infrastructure.Factory
{
    public interface ILevelFactory : IService
    {
        event Action<Level> LevelGenerated;
        void Generate();
    }
}