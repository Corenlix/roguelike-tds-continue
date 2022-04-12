using Entities;
using Entities.Enemies;
using Entities.Weapons;
using Items;
using LevelGeneration;
using Pathfinding;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        Player CreatePlayer(Vector3 at);
        PlayerCamera CreatePlayerCamera();
        GameObject CreateHud();
        Pathfinder CreatePathfinder(Level level);
        Enemy CreateEnemy(EnemyId id, Vector3 at);
        Weapon CreateWeapon(WeaponId id, Transform parent, Vector3 position);
        Bullet CreateBullet(BulletId id, Vector3 position, Quaternion rotation);
        Item CreateItem(ItemId id, Vector3 position);
        Chest CreateChest(ChestId id, Vector3 position);
    }
}