using Entities.Enemies;
using Infrastructure.Factory;
using Infrastructure.StaticData;
using Items;
using LevelGeneration;
using UnityEngine;

namespace GameState
{
    public class LoadLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private LevelStaticData _levelStaticData;

        public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }
        
        public void Enter()
        {
            GenerateLevel();
            _gameFactory.CreatePlayerCamera();
            _gameFactory.CreateHud();
        }

        private void GenerateLevel()
        {
            var level = _gameFactory.CreateLevel(LevelId.FirstLevel);
            SpawnEntities(level);
        }

        private void SpawnEntities(Level level)
        {
            var floorPoints = level.GetFloorPoints();
            _gameFactory.CreatePlayer(floorPoints[0]);
            _gameFactory.CreateEnemySpawner(EnemySpawnerId.FirstLevelSpawner);

            _gameFactory.CreateItem(ItemId.PistolAmmoMediumPack, floorPoints[1]);
            _gameFactory.CreateChest(ChestId.AmmoChest, floorPoints[50]);
            _gameFactory.CreateChest(ChestId.AmmoChest, floorPoints[10]);
            _gameFactory.CreateChest(ChestId.AmmoChest, floorPoints[130]);
            _gameFactory.CreateChest(ChestId.WeaponChest, floorPoints[80]);
            _gameFactory.CreateChest(ChestId.WeaponChest, floorPoints[100]);

            _gameStateMachine.Enter<GameLoopState>();
        }
        
        public void Exit()
        {
            
        }
    }
}