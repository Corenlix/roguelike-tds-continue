using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.HitBoxes
{
    public class PlayerHitBox : HitBox
    {
        public override HitBoxType HitBoxType => HitBoxType.Player;

        [SerializeField] private Health _health;
        [SerializeField] private RigidbodyMover _rigidbodyMover;

        protected override void Hit(HitData hitData, Transform bullet, GameObject sparkles)
        {
            _rigidbodyMover.AddForce(new Force(hitData.KnockBack, bullet.right));
            _health.TakeDamage(hitData.Damage);
        }
    }
}
