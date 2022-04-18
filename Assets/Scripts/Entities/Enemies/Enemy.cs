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
        protected abstract EnemyView View { get; }
        protected Transform Target;

        [SerializeField] private Health _health;
        private EnemyStaticData _enemyStaticData;
        private Player _player;
        private StateMachine _stateMachine;


        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }

        public void Init(EnemyStaticData staticData)
        {
            _enemyStaticData = staticData;
        }
        
        private void Start()
        {
            Target = _player.transform;
            _stateMachine = InitStateMachine(_enemyStaticData);
        }

        private void OnEnable()
        {
            _health.Damaged += View.OnTakeDamage;
            _health.Died += OnDie;
        }

        protected abstract StateMachine InitStateMachine(EnemyStaticData staticData);

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void OnDie()
        {
            _health.Died -= OnDie;
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            _health.Died -= OnDie;
            _health.Damaged -= View.OnTakeDamage;
        }
    }
}
