using UnityEngine;

namespace Entities.Weapons
{
    public class Pistol : Weapon
    {
        protected override WeaponStaticData StaticData => _weaponStaticData;
        [SerializeField] private Bullet _bulletPrefab;
        private PistolStaticData _weaponStaticData;

        public override void Init(WeaponStaticData weaponStaticData)
        {
            _weaponStaticData = (PistolStaticData) weaponStaticData;
        }

        protected override void OnShoot()
        {
            Instantiate(_bulletPrefab, shootPoint.position, transform.RotationWithFlip());
        }
    }
}

