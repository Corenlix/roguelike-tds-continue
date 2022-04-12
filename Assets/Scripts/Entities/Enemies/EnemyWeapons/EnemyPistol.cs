using Entities.Enemies.EnemyAbilities;

namespace Entities.Enemies.EnemyWeapons
{
    public class EnemyPistol : EnemyWeapon
    {
        protected override void OnShoot()
        {
            InstantiateBullet();
        }
    }
}