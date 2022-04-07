using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        public float ShakeIntensity => _shakeIntensity;

        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _shakeIntensity;
        [SerializeField] private float _delay;
        [SerializeField] private AmmoType _ammoType;
        private Vector3 _targetPosition;
        private float _timeRemainToShoot;

        public void AimTo(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            transform.rotation = transform.LookAt2DWithFlip(_targetPosition);
        }
        
        public bool TryShoot()
        {
            if (_timeRemainToShoot > 0)
                return false;
            
            SpawnBullet();
            _timeRemainToShoot = _delay;
            return true;
        }

        private void Update()
        {
            _timeRemainToShoot -= Time.deltaTime;
        }

        private void SpawnBullet()
        {
            Bullet bullet =  Instantiate(_bulletPrefab, _shootPoint.position, transform.RotationWithFlip());
        }
    }
}

