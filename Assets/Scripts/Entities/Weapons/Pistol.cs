using Entities.Weapons.StaticData;

namespace Entities.Weapons
{
    public class Pistol : Weapon
    {
        public override WeaponStaticData StaticData => _weaponStaticData;
        private PistolStaticData _weaponStaticData;

        public override void Init(WeaponStaticData weaponStaticData)
        {
            _weaponStaticData = (PistolStaticData) weaponStaticData;
        }

        protected override void OnShoot()
        {
            CreateBullet();
        }
    }
}

