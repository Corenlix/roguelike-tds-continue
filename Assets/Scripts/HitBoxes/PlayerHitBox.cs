using UnityEngine;

namespace HitBoxes
{
    public class PlayerHitBox : HitBox
    {
        public override HitBoxType HitBoxType => HitBoxType.Player;

        [SerializeField] private Health _health;
        [SerializeField] private RigidbodyMover _rigidbodyMover;

        protected override void Hit(HitData hitData)
        {
            _rigidbodyMover.AddForce(new Force(hitData.KnockBack, hitData.Bullet.right));
            _health.DealDamage(hitData.Damage);
        }
    }
}
