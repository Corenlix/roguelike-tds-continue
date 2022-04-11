using Entities;
using Infrastructure;
using UnityEngine;
using Zenject;

public class GameFactory : IGameFactory
{
        private DiContainer _diContainer;

        public GameFactory(DiContainer container)
        {
                _diContainer = container;
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
}