using Entities;
using Entities.Enemies;
using Entities.Weapons;
using Infrastructure.AssetProvider;
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

                public GameFactory(DiContainer container, IStaticDataService staticDataService, IAssetProvider assetProvider)
                {
                        _assetProvider = assetProvider;
                        _diContainer = container;
                        _staticDataService = staticDataService;
                }
        
                public Player CreatePlayer(Vector3 at)
                {
                        _diContainer.Bind<PlayerWeapons>().AsSingle();
                        _diContainer.Bind<PlayerAmmoBelt>().AsSingle();
                        var player = _assetProvider.Instantiate<Player>(AssetPath.Player, at);
                        _diContainer.Bind<Player>().FromInstance(player).AsSingle();
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

                public Enemy CreateEnemy(EnemyId id, Vector3 at)
                {
                        if (id == EnemyId.None)
                                return null;
                
                        var enemyData = _staticDataService.ForEnemy(id);
                        var enemy = _diContainer.InstantiatePrefabForComponent<Enemy>(enemyData.Prefab, at,
                                Quaternion.identity, null);
                        enemy.Init(enemyData);
                        return enemy;
                }

                public Pathfinder CreatePathfinder(Level level)
                {
                        var pathfinder = new Pathfinder(level);
                        _diContainer.Bind<Pathfinder>().FromInstance(pathfinder).AsSingle();
                        return pathfinder;
                }

                public Weapon CreateWeapon(WeaponId id, Transform parent, Vector3 position)
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

                public Bullet CreateBullet(BulletId id, Vector3 position, Quaternion rotation)
                {
                        if (id == BulletId.None)
                                return null;
                
                        var bulletData = _staticDataService.ForBullet(id);
                        var bullet = _diContainer.InstantiatePrefabForComponent<Bullet>(bulletData.Prefab, position, rotation, null);
                        bullet.Init(bulletData);
                        return bullet;
                }

                public Item CreateItem(ItemId id, Vector3 position)
                {
                        if (id == ItemId.None)
                                return null;
                
                        var itemData = _staticDataService.ForItem(id);
                        var item = _diContainer.InstantiatePrefabForComponent<Item>(itemData.Prefab, position, Quaternion.identity, null);
                        return item;
                }

                public Chest CreateChest(ChestId id, Vector3 position)
                {
                        if (id == ChestId.None)
                                return null;

                        var chestData = _staticDataService.ForChest(id);
                        var chest = _diContainer.InstantiatePrefabForComponent<Chest>(chestData.Prefab, position, Quaternion.identity, null);
                        chest.Init(chestData);
                        return chest;
                }
        }
}