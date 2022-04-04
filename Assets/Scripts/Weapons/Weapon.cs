using System;
using UnityEngine;

namespace Weapons
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

        public void AimTo(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            transform.rotation = transform.LookAt2DWithFlip(_targetPosition);
        }
        
        public void Shoot()
        {
            if (_timeRemainToShoot > 0)
                return;
            
            SpawnBullet();
            _timeRemainToShoot = _delay;
        }

        private void Update()
        {
            _timeRemainToShoot -= Time.deltaTime;
        }

        private void SpawnBullet()
        {
            Bullet bullet =  Instantiate(_bulletPrefab, _shootPoint.position, transform.RotationWithFlip());
            bullet.Init(_interactiveLayers);
        }
    }
}

