using Infrastructure;
using Items;
using LevelGeneration;
using UnityEngine;
using Zenject;

namespace GameState
{
    public class GenerateLevelState : IState
    {
        private GameStateMachine _gameStateMachine;
        private ILevelFactory _levelFactory;
        private IGameFactory _gameFactory;
        private readonly IStaticDataService _staticData;

        public GenerateLevelState(GameStateMachine gameStateMachine, ILevelFactory levelFactory, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _levelFactory = levelFactory;
        }
        
        public void Enter()
        {
            _levelFactory.Generate();
            _levelFactory.LevelGenerated += OnLevelGenerate;
        }

        private void OnLevelGenerate(Level level)
        {
            _levelFactory.LevelGenerated -= OnLevelGenerate;
            _gameFactory.CreatePathfinder(level);
            _gameFactory.CreatePlayer(level.MainRooms[0].Rect.center);
            _gameFactory.CreatePlayerCamera();
            _gameFactory.CreateHud();
            _gameFactory.CreateItem(ItemId.PistolAmmoMediumPack, level.MainRooms[0].Rect.center + Vector2.right * 4);
            _gameFactory.CreateEnemy(level.MainRooms[1].Rect.center, EnemyId.Test);
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            
        }
    }
}