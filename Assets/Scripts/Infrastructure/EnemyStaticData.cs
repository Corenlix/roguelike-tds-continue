using Entities.Enemies;
using UnityEngine;

namespace Infrastructure
{
    public abstract class EnemyStaticData : ScriptableObject
    {
        public EnemyId Id;
        public Enemy Prefab;
    }
}