using Entities.Enemies;
using Entities.Enemies.StaticData;
using Entities.Weapons;
using Entities.Weapons.StaticData;
using Items;
using LevelGeneration;

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
        LevelStaticData ForLevel(LevelId id);
        EnemySpawnerStaticData ForEnemySpawner(EnemySpawnerId id);
        ItemStaticData ForItem(WeaponId id);
    }
}