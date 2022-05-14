using System.Collections.Generic;
using Entities;
using Entities.Enemies;
using Infrastructure.Factory;
using Infrastructure.Progress;
using Infrastructure.SaveLoad;
using LevelGeneration;
using UnityEditor.SearchService;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameState
{
    public class LoadLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private Pillar _pillar;
        private Enemy _boss;
        private Player _player;
        private WormHole _wormHole;

        public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory, IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }
        
        public void Enter()
        {
            GenerateLevel();
            _progressService.Load();
            _gameFactory.CreatePlayerCamera();
            _gameFactory.CreateHud();
        }
        
        private void GenerateLevel()
        {
            var levelId = _saveLoadService.GetValue(SaveLoadKey.Level, LevelId.FirstLevel);
            var level = _gameFactory.CreateLevel(levelId);
            SpawnEntities(level);
        }

        private void SpawnEntities(Level level)
        {
            var floorPoints = level.GetFloorPoints();

            _player = _gameFactory.CreatePlayer(floorPoints[0]);
            _player.Health.Died += OnPlayerDied;
            _gameFactory.CreateEnemySpawner(EnemySpawnerId.FirstLevelSpawner);
            
            _gameFactory.CreateChest(ChestId.AmmoChest, floorPoints[50]);
            _gameFactory.CreateChest(ChestId.AmmoChest, floorPoints[10]);
            _gameFactory.CreateChest(ChestId.AmmoChest, floorPoints[130]);
            _gameFactory.CreateChest(ChestId.WeaponChest, floorPoints[80]);
            _gameFactory.CreateChest(ChestId.WeaponChest, floorPoints[100]);
            
            var pillar = _gameFactory.CreatePillar(EnemyId.EyeBoss, floorPoints[200]);
            _pillar = pillar;
            _pillar.BossSpawned += OnBossSpawned;
        }

        private void OnBossSpawned(Pillar pillar, Enemy boss)
        {
            pillar.BossSpawned -= OnBossSpawned;
            _boss = boss;
            _boss.Died += OnBossDied;
        }

        private void OnBossDied(Enemy boss)
        {
            boss.Died -= OnBossDied;
            _wormHole = _gameFactory.CreateWormHole(_boss, _boss.transform.position);
            _wormHole.TransitionNextLevel += TransitNextLevel;
        }

        private void TransitNextLevel()
        {
            _gameStateMachine.Enter<LoadNextLevelState>();
            _wormHole.TransitionNextLevel -= TransitNextLevel;
        }
        
        private void OnPlayerDied()
        {
            _player.Health.Died -= OnPlayerDied;
            if(_pillar)
                _pillar.BossSpawned -= OnBossSpawned;
            if (_boss)
                _boss.Died -= OnBossDied;
            
            _gameStateMachine.Enter<LooseState>();
        }

        public void Exit()
        {
            
        }
    }
}