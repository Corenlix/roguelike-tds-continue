using Entities;
using Entities.Enemies;
using LevelGeneration;
using Pathfinding;
using UnityEngine;

namespace Infrastructure
{
    public interface IGameFactory
    {
        Player CreatePlayer(Vector3 at);
        PlayerCamera CreatePlayerCamera();
        GameObject CreateHud();
        Pathfinder CreatePathfinder(Level level);
        Enemy CreateEnemy(Vector3 at, EnemyId id);
    }
}