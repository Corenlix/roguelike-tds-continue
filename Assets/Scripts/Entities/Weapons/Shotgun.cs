using UnityEngine;

namespace Entities.Weapons
{
    public class Shotgun : Weapon
    {
        private const int BulletsCount = 5;
        private const float MaxDeviationAngle = 10;
        [SerializeField] private Bullet _bulletPrefab;

        protected override void OnShoot()
        {
            float angleBetweenNeighborBullets = MaxDeviationAngle * 2 / (BulletsCount - 1);
            for (int i = 0; i < BulletsCount; i++)
            {
                float angle = -MaxDeviationAngle + i * angleBetweenNeighborBullets;
                InstantiateBullet(angle);
            }
        }

        private void InstantiateBullet(float deviationDegrees)
        {
            var bullet = Instantiate(_bulletPrefab, shootPoint.transform.position, transform.RotationWithFlip(deviationDegrees));
        }
    }
}