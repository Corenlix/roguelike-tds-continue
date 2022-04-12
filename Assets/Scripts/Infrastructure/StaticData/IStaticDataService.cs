using Entities.Enemies;
using Entities.Enemies.StaticData;
using Entities.Weapons;
using Entities.Weapons.StaticData;
using Items;

namespace Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        public EnemyStaticData ForEnemy(EnemyId id);
        WeaponStaticData ForWeapon(WeaponId id);
        BulletStaticData ForBullet(BulletId id);
        ItemStaticData ForItem(ItemId id);
        LootStaticData ForLoot(LootId id);
        ChestStaticData ForChest(ChestId id);
    }
}