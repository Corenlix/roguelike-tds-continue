using Entities.Enemies;
using UnityEngine;

namespace Infrastructure
{
    public abstract class EnemyStaticData : ScriptableObject
    {
        public abstract Enemy Prefab { get; }
        public EnemyId Id;
    }
}