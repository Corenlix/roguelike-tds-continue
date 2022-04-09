using UnityEngine;

namespace Entities.Weapons
{
    public class Pistol : Weapon
    {
        [SerializeField] private Bullet _bulletPrefab;
        
        protected override void OnShoot()
        {
            Instantiate(_bulletPrefab, shootPoint.position, transform.RotationWithFlip());
        }
    }
}

