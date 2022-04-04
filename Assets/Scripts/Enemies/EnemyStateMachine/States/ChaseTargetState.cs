using System;
using UnityEngine;
using Weapons;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Enemies.EnemyStateMachine.States
{
    public class ChaseTargetState : State
    {
        private readonly float _minWalkPeriod;
        private readonly float _maxWalkPeriod;
        private readonly PathfindMover _pathfindMover;
        private readonly Transform _transform;
        private readonly Transform _target;
        private readonly EntityView _entityView;
        private Weapon _weapon;
        
        private float _timeRemainToWalk;

        public ChaseTargetState(Transform transform, EntityView entityView, Weapon weapon, Transform target, PathfindMover pathfindMover, float minWalkPeriod, float maxWalkPeriod)
        {
            _transform = transform;
            _pathfindMover = pathfindMover;
            _minWalkPeriod = minWalkPeriod;
            _maxWalkPeriod = maxWalkPeriod;
            _target = target;
            _entityView = entityView;
            _weapon = weapon;
        }

        public override void Enter()
        {
        }
    
        public override void Tick()
        {
            TryMove();
            TryAttack();
        }

        private void TryMove()
        {
            _timeRemainToWalk -= Time.deltaTime;
            if (_timeRemainToWalk > 0)
                return;
        
            _pathfindMover.SetMovePoint(GetDestination());
            _timeRemainToWalk = Random.Range(_minWalkPeriod, _maxWalkPeriod);
        }

        private void TryAttack()
        {
            var targetPos = _target.position;
            _entityView.LookAt(targetPos);
            _weapon.AimTo(targetPos);
            _weapon.Shoot();
        }
        
        private Vector2 GetDestination()
        {
            var currentPosition = _transform.position;
            var targetPosition = _target.transform.position;
            return new Vector2(Random.Range(Mathf.Min(currentPosition.x, targetPosition.x) - 2, Mathf.Max(currentPosition.x, targetPosition.x) + 2),
                Random.Range(Mathf.Min(currentPosition.y, targetPosition.y) - 2, Mathf.Max(currentPosition.y, targetPosition.y) + 2));
        }
    
        public override void Exit()
        {
            _pathfindMover.Reset();
        }
    }
}
