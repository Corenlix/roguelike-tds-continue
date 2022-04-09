using Popup;
using UnityEngine;

namespace Entities.HitBoxes
{
    public class EnemyHitBox : HitBox
    {
        public override HitBoxType HitBoxType => HitBoxType.Enemy;
        
        [SerializeField] private Health _health;
        [SerializeField] private RigidbodyMover _rigidbodyMover;

        protected override void Hit(HitData hitData)
        {
            _health.DealDamage(hitData.Damage);
            _rigidbodyMover.AddForce(new Force(hitData.KnockBack, hitData.Bullet.right));
            PopupSpawner.Instance.SpawnPopup(PopupType.Damage, transform.position, hitData.Damage.ToString());
        }
    }
}