namespace Entities.Enemies.EnemyWeapons
{
    public class EnemyPistol : EnemyShootWeapon
    {
        protected override void OnShoot()
        {
            InstantiateBullet();
        }
    }
}