using UnityEngine;

namespace Entities.Enemies.StaticData
{
    [CreateAssetMenu(menuName = "Static Data/Enemies/Melee Enemy", fileName = "Melee Enemy")]
    public class MeleeEnemyStaticData : EnemyStaticData
    {
        public override Enemy Prefab => _prefab;
        [Header("Behavior")]
        public int RandomWalkDistance;
        public  float RandomWalkPeriod;
        public  int WalkToChaseDistance;
        public  int ChaseToWalkDistance;
        public int ChaseToAttackDistance;
        public int AttackToChaseDistance;
        [SerializeField] private MeleeEnemy _prefab;
    }
}
