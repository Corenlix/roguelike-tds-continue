using Entities.HitBoxes;
using UnityEngine;

namespace Entities.Enemies.StaticData
{
    public abstract class EnemyStaticData : ScriptableObject
    {
        public abstract Enemy Prefab { get; }

        public EnemyId Id;
        public float MoveSpeed;
        public HitData HitData;
        public float BulletSpeed;
        public float ShootDelay;
    }
}