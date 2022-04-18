using Entities.Enemies.StaticData;
using UnityEngine;

namespace Entities.Enemies
{
    [CreateAssetMenu(menuName = "Static Data/Enemies/Eye Boss", fileName = "Eye Boss")]
    public class EyeStaticData : EnemyStaticData
    {
        [Range(1, 6)]
        public float DashBoost;
        public float DashTime;
        public float ChaseTime;
        public float RotateSpeed;
        
        public override Enemy Prefab => _prefab;
        [SerializeField] private EyeBoss _prefab;
    }
}