using Entities.HitBoxes;
using UnityEngine;

namespace Entities.Enemies.EnemyWeapons
{
    public abstract class EnemyShootWeapon : EnemyWeapon
    {
        [SerializeField] protected Transform _shootPoint;
        [SerializeField] private EnemyBullet _enemyBullet;
        private float _delay;
        private float _timeRemainToShoot;
        private float _bulletSpeed;
        private HitData _hitData;

        public void Init(HitData hitData, float bulletSpeed, float delay)
        {
            _hitData = hitData;
            _bulletSpeed = bulletSpeed;
            _delay = delay;
        }
        
        public override bool TryHit()
        {
            if (_timeRemainToShoot > 0)
                return false;

            OnShoot();
            _timeRemainToShoot = _delay;
            return true;
        }

        protected abstract void OnShoot();

        public void AimTo(Vector3 targetPosition)
        {
            transform.rotation = transform.LookAt2DWithFlip(targetPosition);
        }

        protected EnemyBullet InstantiateBullet(float addAngle = 0)
        {
            var bullet = Instantiate(_enemyBullet, _shootPoint.position, transform.RotationWithFlip(addAngle));
            bullet.Init(_hitData, _bulletSpeed);
            return bullet;
        }
        
        private void Update()
        {
            _timeRemainToShoot -= Time.deltaTime;
        }
    }
}