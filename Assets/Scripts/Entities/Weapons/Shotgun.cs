namespace Entities.Weapons
{
    public class Shotgun : Weapon
    {
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
    }
}