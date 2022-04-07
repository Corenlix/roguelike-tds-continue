using Popup;
using UnityEngine;

namespace HitBoxes
{
    public class EnemyHitBox : HitBox
    {
        public override HitBoxType HitBoxType => HitBoxType.Enemy;
        
        [SerializeField] private Health _health;

        protected override void Hit(HitData hitData)
        {
            _health.DealDamage(hitData.Damage);
            PopupSpawner.Instance.SpawnPopup(transform.position, hitData.Damage);
        }
    }
}