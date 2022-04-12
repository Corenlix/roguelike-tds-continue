using UnityEngine;

namespace Entities.Weapons
{
    public class Shotgun : Weapon
    {
        [SerializeField] private Bullet _bulletPrefab;
        private ShotgunStaticData _shotgunStaticData;

        protected override WeaponStaticData StaticData => _shotgunStaticData;
        
        public override void Init(WeaponStaticData weaponStaticData)
        {
            _shotgunStaticData = (ShotgunStaticData) weaponStaticData;
        }

        protected override void OnShoot()
        {
            float angleBetweenNeighborBullets = _shotgunStaticData.MaxDeviationAngle * 2 / (_shotgunStaticData.BulletsCount - 1);
            for (int i = 0; i < _shotgunStaticData.BulletsCount; i++)
            {
                float angle = -_shotgunStaticData.MaxDeviationAngle + i * angleBetweenNeighborBullets;
                InstantiateBullet(angle);
            }
        }

        private void InstantiateBullet(float deviationDegrees)
        {
            var bullet = Instantiate(_bulletPrefab, shootPoint.transform.position, transform.RotationWithFlip(deviationDegrees));
        }
    }
}