using Entities.Enemies;
using UnityEngine;

namespace Infrastructure
{
    [CreateAssetMenu(menuName = "Static Data/Enemies/Range Enemy", fileName = "Range Enemy")]
    public class RangeEnemyStaticData : EnemyStaticData
    {
        [Header("Behavior")]
        public int RandomWalkDistance;
        public  float RandomWalkPeriod;
        public  int ChaseToIdleDistance;
        public  int IdleToChaseDistance;
        public  int WalkToChaseDistance;
        public  int ChaseToWalkDistance;
    }
}