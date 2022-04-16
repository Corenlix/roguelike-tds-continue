using System.Collections;
using Entities.Weapons.StaticData;
using UnityEngine;

namespace Entities.Weapons
{
    public class AssaultRiffle : Weapon
    {
        private const float TimeBetweenShootsInBurst = 0.05f;
        private const float DeltaAngle = 0.5f;
    
        protected override WeaponStaticData StaticData => _assaultRifleStaticData;

        private AssaultRifleStaticData _assaultRifleStaticData;
    
        public override void Init(WeaponStaticData weaponStaticData)
        {
            _assaultRifleStaticData = (AssaultRifleStaticData) weaponStaticData;
        }

        protected override void OnShoot()
        {
            for (int i = 0; i < _assaultRifleStaticData.ShootsInBurst; i++)
            {
                StartCoroutine(ShootCoroutine(TimeBetweenShootsInBurst * i));
            }
        }

        private IEnumerator ShootCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            InstantiateBullet(Random.Range(-DeltaAngle, DeltaAngle));
        }
    }
}