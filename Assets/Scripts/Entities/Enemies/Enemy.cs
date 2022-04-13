using System;
using Entities.Enemies.EnemyWeapons;
using Entities.Enemies.StaticData;
using Infrastructure;
using Pathfinding;
using UnityEngine;
using Zenject;

namespace Entities.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private PathfindMover _pathfindMover;
        [SerializeField] private EntityView _view;
        [SerializeField] private Health _health;
        [SerializeField] private EnemyWeapon _enemyWeapon;
        private EnemyStaticData _enemyStaticData;
        private Pathfinder _pathfinder;
        private Player _player;
        
        protected Transform Target;

        [Inject]
        private void Construct(Pathfinder pathfinder, Player player)
        {
            _pathfinder = pathfinder;
            _player = player;
        }

        public void Init(EnemyStaticData staticData)
        {
            _enemyStaticData = staticData;
        }

        private void OnEnable()
        {
            _health.Damaged += OnTakeDamage;
        }

        private void Start()
        {
            SetTarget(_player.transform);
            _health.Died += OnDie;
            _pathfindMover.Init(_pathfinder, _enemyStaticData.MoveSpeed);
            _enemyWeapon.Init(_enemyStaticData.HitData, _enemyStaticData.BulletSpeed, _enemyStaticData.ShootDelay);
            OnInit(_enemyStaticData);
        }

        protected abstract void OnInit(EnemyStaticData staticData);

        public void SetTarget(Transform target)
        {
            Target = target;
        }

        public void Attack(Vector3 target)
        {
            _view.LookTo(target);
            _enemyWeapon.AimTo(target);
            _enemyWeapon.TryShoot();
        }

        public void MoveTo(Vector3 destination) => _pathfindMover.SetMovePoint(destination);

        public void Stop() => _pathfindMover.Reset();

        private void OnDie()
        {
            _health.Died -= OnDie;
            Destroy(gameObject);
        }

        private void OnTakeDamage()
        {
            _view.OnTakeDamage();
        }
    }
}
