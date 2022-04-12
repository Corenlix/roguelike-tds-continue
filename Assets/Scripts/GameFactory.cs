using Entities;
using Entities.Enemies;
using Infrastructure;
using LevelGeneration;
using Pathfinding;
using UnityEngine;
using Zenject;

public class GameFactory : IGameFactory
{
        private readonly DiContainer _diContainer;
        private readonly IStaticDataService _staticDataService;

        public GameFactory(DiContainer container, IStaticDataService staticDataService)
        {
                _diContainer = container;
                _staticDataService = staticDataService;
        }
        
        public Player CreatePlayer(Vector3 at)
        {
                var player = _diContainer.InstantiatePrefabForComponent<Player>(Resources.Load<Player>("Player"), at, Quaternion.identity, null);
                _diContainer.Bind<Player>().FromInstance(player).AsSingle();
                return player;
        }

        public PlayerCamera CreatePlayerCamera()
        {
                var camera =
                        _diContainer.InstantiatePrefabForComponent<PlayerCamera>(
                                Resources.Load<PlayerCamera>("PlayerCamera"));
                _diContainer.Bind<CameraShaker>().FromInstance(camera.Shaker).AsSingle();
                return camera;
        }

        public GameObject CreateHud()
        {
                var hud = _diContainer.InstantiatePrefab(Resources.Load("Hud"));
                return hud;
        }

        public Enemy CreateEnemy(Vector3 at, EnemyId id)
        {
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
}