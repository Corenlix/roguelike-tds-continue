namespace Entities.Weapons
{
    public class Pistol : Weapon
    {
        protected override WeaponStaticData StaticData => _weaponStaticData;
        private PistolStaticData _weaponStaticData;

        public override void Init(WeaponStaticData weaponStaticData)
        {
            _weaponStaticData = (PistolStaticData) weaponStaticData;
        }

        protected override void OnShoot()
        {
            InstantiateBullet();
        }
    }
}

