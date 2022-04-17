using UnityEngine;

namespace Entities.Enemies.StaticData
{
    [CreateAssetMenu(menuName = "Static Data/Enemies/Range Enemy", fileName = "Range Enemy")]
    public class RangeEnemyStaticData : EnemyStaticData
    {
        public override Enemy Prefab => _prefab;
        [Header("Behavior")]
        public int RandomWalkDistance;
        public  float RandomWalkPeriod;
        public  int ChaseToIdleDistance;
        public  int IdleToChaseDistance;
        public  int WalkToChaseDistance;
        public  int ChaseToWalkDistance;
        [SerializeField] private RangeEnemy _prefab;
        
        [Header("Shooting")]
        public float BulletSpeed;
        public float ShootDelay;
    }
}