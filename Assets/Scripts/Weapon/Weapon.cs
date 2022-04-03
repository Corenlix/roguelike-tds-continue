using UnityEngine;

namespace Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private float _delay;
        [SerializeField] private AmmoType _ammoType;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private LayerMask _interactiveLayers;

        private Vector3 _targetPosition;
        private float _timeRemainToShoot;

        public void Shoot()
        {
            _timeRemainToShoot -= Time.deltaTime;
            if (_timeRemainToShoot > 0)
                return;

            SpawnBullet();
            _timeRemainToShoot = _delay;
        }

        private void SpawnBullet()
        {
            Bullet bullet =  Instantiate(_bulletPrefab, _shootPoint.position, transform.RotationWithFlip());
            bullet.Init(_interactiveLayers);
        }
        
        public void AimTo(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            transform.rotation = transform.LookAt2DWithFlip(_targetPosition);
        }
    }
}

