using System.Collections.Generic;
using Entities;
using Entities.Enemies;
using Entities.Weapons;
using Items;
using LevelGeneration;
using Pathfinding;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        Player CreatePlayer(Vector2 at);
        PlayerCamera CreatePlayerCamera();
        GameObject CreateHud();
        Enemy CreateEnemy(EnemyId id, Vector2 at);
        Weapon CreateWeapon(WeaponId id, Transform parent, Vector2 position);
        Bullet CreateBullet(BulletId id, Vector2 position, Quaternion rotation);
        Item CreateItem(ItemId id, Vector2 position);
        Chest CreateChest(ChestId id, Vector2 position);
        Level CreateLevel(LevelId id);
        EnemiesSpawner CreateEnemySpawner(EnemySpawnerId id);
        List<Item> CreateItemsForLoot(LootId id, Vector3 position);
        Pillar CreatePillar(EnemyId id, Vector2 position);
        Item CreateItem(WeaponId id, Vector2 position);
        WormHole CreateWormHole(Enemy enemy, Vector2 position);
    }
}