using Entities;
using Infrastructure;
using UnityEngine;
using Zenject;

public class PlayerFactory : IPlayerFactory
{
        public Player Player { get; }

        private DiContainer _diContainer;

        public PlayerFactory(DiContainer container)
        {
                _diContainer = container;
                Player = _diContainer.InstantiatePrefabForComponent<Player>(Resources.Load<Player>("Player"));
        }
}