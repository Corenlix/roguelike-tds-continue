using UnityEngine;

namespace Entities.Enemies.EnemyWeapons
{
    public abstract class EnemyWeapon : MonoBehaviour
    {
        public abstract bool TryHit();
    }
}