using Entities.Enemies;
using Infrastructure.Factory;
using Infrastructure.StaticData;
using Items;
using LevelGeneration;

namespace GameState
{
    public class LoadLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticDataService;
        private LevelStaticData _levelStaticData;

        public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
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
            _levelStaticData = _staticDataService.ForLevel(LevelId.FirstLevel);
            var level = _levelStaticData.Generate();
            new LevelDrawer().DrawLevel(level.LevelTable);
            _gameFactory.CreatePathfinder(level);
            SpawnEntities(level);
        }

        private void SpawnEntities(Level level)
        {
            var floorPoints = level.GetFloorPoints();
            _gameFactory.CreatePlayer(floorPoints[0]);
            _gameFactory.CreateItem(ItemId.Shotgun, floorPoints[1]);
            _gameFactory.CreateEnemy(EnemyId.Spider, floorPoints[20]);
            _gameFactory.CreateChest(ChestId.AmmoChest, floorPoints[50]);
            _gameFactory.CreateChest(ChestId.WeaponChest, floorPoints[80]);
            _gameStateMachine.Enter<GameLoopState>();
        }
        
        public void Exit()
        {
            
        }
    }
}