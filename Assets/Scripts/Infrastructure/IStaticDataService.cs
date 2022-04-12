using Entities.Weapons;
using Items;

namespace Infrastructure
{
    public interface IStaticDataService
    {
        public EnemyStaticData ForEnemy(EnemyId id);
        WeaponStaticData ForWeapon(WeaponId id);
        BulletStaticData ForBullet(BulletId id);
        ItemStaticData ForItem(ItemId id);
    }
}