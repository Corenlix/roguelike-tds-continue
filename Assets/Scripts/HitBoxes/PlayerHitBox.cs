using UnityEngine;

namespace HitBoxes
{
    public class PlayerHitBox : HitBox
    {
        public override HitBoxType HitBoxType => HitBoxType.Player;

        [SerializeField] private Health _health;

        protected override void Hit(HitData hitData)
        {
            _health.DealDamage(hitData.Damage);
        }
    }
}
