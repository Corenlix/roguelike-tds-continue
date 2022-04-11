using System;
using LevelGeneration;

namespace Infrastructure
{
    public interface ILevelFactory
    {
        event Action LevelGenerated;
        Level Level { get; }
    }
}