using System.Collections.Generic;
using Entities;
using Entities.Enemies;
using Entities.Weapons;
using Infrastructure.AssetProvider;
using Infrastructure.Progress;
using Infrastructure.StaticData;
using Items;
using LevelGeneration;
using Pathfinding;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factory
{
        public class GameFactory : IGameFactory
        {
                private readonly DiContainer _diContainer;
                private readonly IStaticDataService _staticDataService;
                private readonly IAssetProvider _assetProvider;
                private readonly IProgressService _progressService;

                public GameFactory(DiContainer container, IStaticDataService staticDataService,
                        IAssetProvider assetProvider, IProgressService progressService)
                {
                        _progressService = progressService;
                        _assetProvider = assetProvider;
                        _diContainer = container;
                        _staticDataService = staticDataService;
                }

                public Player CreatePlayer(Vector2 at)
                {
                        _diContainer.Bind<PlayerAmmoBelt>().AsSingle();
                        _diContainer.Bind<PlayerWeapons>().AsSingle();
                        var player = _assetProvider.Instantiate<Player>(AssetPath.Player, at);
                        _diContainer.Bind<Player>().FromInstance(player).AsSingle();
                        _progressService.AddClient(_diContainer.Resolve<PlayerAmmoBelt>());
                        _progressService.AddClient(_diContainer.Resolve<PlayerWeapons>());
                        return player;
                }

                public PlayerCamera CreatePlayerCamera()
                {
                        var camera =
                                _assetProvider.Instantiate<PlayerCamera>(AssetPath.PlayerCamera);
                        _diContainer.Bind<CameraShaker>().FromInstance(camera.Shaker).AsSingle();
                        return camera;
                }

                public GameObject CreateHud()
                {
                        var hud = _assetProvider.Instantiate(AssetPath.Hud);
                        return hud;
                }

                public Enemy CreateEnemy(EnemyId id, Vector2 at)
                {
                        if (id == EnemyId.None)
                                return null;

                        var enemyData = _staticDataService.ForEnemy(id);
                        var enemy = _diContainer.InstantiatePrefabForComponent<Enemy>(enemyData.Prefab, at,
                                Quaternion.identity, null);
                        enemy.Init(enemyData);
                        return enemy;
                }

                public Weapon CreateWeapon(WeaponId id, Transform parent, Vector2 position)
                {
                        if (id == WeaponId.None)
                                return null;

                        var weaponData = _staticDataService.ForWeapon(id);
                        var weapon =
                                _diContainer.InstantiatePrefabForComponent<Weapon>(weaponData.Prefab, position,
                                        Quaternion.identity, parent);
                        weapon.Init(weaponData);
                        return weapon;
                }

                public Bullet CreateBullet(BulletId id, Vector2 position, Quaternion rotation)
                {
                        if (id == BulletId.None)
                                return null;

                        var bulletData = _staticDataService.ForBullet(id);
                        var bullet =
                                _diContainer.InstantiatePrefabForComponent<Bullet>(bulletData.Prefab, position,
                                        rotation, null);
                        bullet.Init(bulletData);
                        return bullet;
                }

                public Item CreateItem(ItemId id, Vector2 position)
                {
                        if (id == ItemId.None)
                                return null;

                        var itemData = _staticDataService.ForItem(id);
                        var item = _diContainer.InstantiatePrefabForComponent<Item>(itemData.Prefab, position,
                                Quaternion.identity, null);
                        return item;
                }
                
                public Item CreateItem(WeaponId id, Vector2 position)
                {
                        if (id == WeaponId.None)
                                return null;

                        var itemData = _staticDataService.ForItem(id);
                        var item = _diContainer.InstantiatePrefabForComponent<Item>(itemData.Prefab, position,
                                Quaternion.identity, null);
                        return item;
                }
                
                public List<Item> CreateItemsForLoot(LootId id, Vector3 position)
                {
                        if (id == LootId.None)
                                return null;

                        var loot = _staticDataService.ForLoot(id);
                        var itemIds = loot.Get();
                        var result = new List<Item>();
                        itemIds.ForEach(id=>result.Add(CreateItem(id, position)));
                        return result;
                }

                public Chest CreateChest(ChestId id, Vector2 position)
                {
                        if (id == ChestId.None)
                                return null;

                        var chestData = _staticDataService.ForChest(id);
                        var chest = _diContainer.InstantiatePrefabForComponent<Chest>(chestData.Prefab, position,
                                Quaternion.identity, null);
                        chest.Init(chestData);
                        _diContainer.Resolve<Level>().SetWallCell(position);
                        
                        return chest;
                }

                public Level CreateLevel(LevelId id)
                {
                        var levelStaticData = _staticDataService.ForLevel(LevelId.FirstLevel);
                        var level = levelStaticData.Generate();
                        _diContainer.Bind<Level>().FromInstance(level).AsSingle();
                        new LevelDrawer().DrawLevel(level.LevelTable, _assetProvider.Load<Material>(AssetPath.WallsMaterial));
                        return level;
                }

                public EnemiesSpawner CreateEnemySpawner(EnemySpawnerId id)
                {
                        var spawner = _diContainer.InstantiateComponentOnNewGameObject<EnemiesSpawner>();
                        spawner.Init(_staticDataService.ForEnemySpawner(id));
                        return spawner;
                }

                public Pillar CreatePillar(EnemyId id, Vector2 position)
                {
                        if (id == EnemyId.None)
                                return null;
                        
                        var pillar = _assetProvider.Instantiate<Pillar>(AssetPath.Pillar, position);
                        pillar.Init(id);
                        _diContainer.Resolve<Level>().SetWallCell(position);
                        
                        return pillar;
                }
        }
}