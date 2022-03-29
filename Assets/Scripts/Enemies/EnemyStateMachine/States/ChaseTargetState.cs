using System;
using UnityEngine;
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
        private readonly Func<Vector2> _targetPositionFunc;
        private readonly EntityView _entityView;
        
        private float _timeRemainToWalk;
        
        public ChaseTargetState(Transform transform, EntityView entityView, Func<Vector2> targetPositionFunc, PathfindMover pathfindMover, float minWalkPeriod, float maxWalkPeriod)
        {
            _transform = transform;
            _pathfindMover = pathfindMover;
            _minWalkPeriod = minWalkPeriod;
            _maxWalkPeriod = maxWalkPeriod;
            _targetPositionFunc = targetPositionFunc;
            _entityView = entityView;
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
            var targetPos = Object.FindObjectOfType<Player>().transform.position;
            _entityView.LookAt(targetPos);

            //attack
        }
        
        private Vector2 GetDestination()
        {
            var currentPosition = _transform.position;
            var targetPosition = _targetPositionFunc();
            return new Vector2(Random.Range(Mathf.Min(currentPosition.x, targetPosition.x) - 3, Mathf.Max(currentPosition.x, targetPosition.x) + 3),
                Random.Range(Mathf.Min(currentPosition.y, targetPosition.y) - 3, Mathf.Max(currentPosition.y, targetPosition.y) + 3));
        }
    
        public override void Exit()
        {
            _pathfindMover.Reset();
        }
    }
}
