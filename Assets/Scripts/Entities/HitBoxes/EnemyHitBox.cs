using Infrastructure.Popup;
using UnityEngine;
using Zenject;

namespace Entities.HitBoxes
{
    public sealed class EnemyHitBox : HitBox
    {
        public override HitBoxType HitBoxType => HitBoxType.Enemy;
        
        [SerializeField] private Health _health;
        [SerializeField] private RigidbodyMover _rigidbodyMover;
        private PopupSpawner _popupSpawner;
        
        [Inject]
        private void Construct(PopupSpawner popupSpawner)
        {
            _popupSpawner = popupSpawner;
        }
        
        protected override void Hit(HitData hitData, Transform bullet, GameObject sparkles = null)
        {
            _health.TakeDamage(hitData.Damage);
            _rigidbodyMover.AddForce(new Force(hitData.KnockBack, bullet.right));
            _popupSpawner.SpawnPopup(PopupType.Damage, transform.position, hitData.Damage.ToString());
        }
    }
}