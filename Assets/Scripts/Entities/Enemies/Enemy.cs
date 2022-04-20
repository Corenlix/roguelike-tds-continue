using System;
using System.Collections.Generic;
using Entities.Enemies.EnemyStateMachine;
using Entities.Enemies.EnemyWeapons;
using Entities.Enemies.StaticData;
using Entities.HitBoxes;
using Infrastructure;
using Infrastructure.Factory;
using Pathfinding;
using UnityEngine;
using Zenject;

namespace Entities.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        protected static List<HitBoxType> DefaultTargetTypes => new List<HitBoxType> { HitBoxType.Player, HitBoxType.Wall };
        protected Transform Target;

        [SerializeField] private Health _health;
        private EnemyStaticData _enemyStaticData;
        private Player _player;
        private StateMachine _stateMachine;
        private IGameFactory _gameFactory;


        [Inject]
        private void Construct(Player player, IGameFactory gameFactory)
        {
            _player = player;
            _gameFactory = gameFactory;
        }

        public void Init(EnemyStaticData staticData)
        {
            _enemyStaticData = staticData;
        }
        
        private void Start()
        {
            _health.Init(_enemyStaticData.Health);
            
            Target = _player.transform;
            _stateMachine = InitStateMachine(_enemyStaticData);
        }

        private void OnEnable()
        {
            _health.Died += OnDie;
        }

        protected abstract StateMachine InitStateMachine(EnemyStaticData staticData);

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void OnDie()
        {
            SpawnLoot();
        }

        private void SpawnLoot() => _gameFactory.CreateItemsForLoot(_enemyStaticData.LootId, transform.position);

        private void OnDisable()
        {
            _health.Died -= OnDie;
        }
    }
}
