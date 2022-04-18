using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Zenject;

namespace Entities
{
    public class PathfindMover : Mover
    {
        [SerializeField] private RigidbodyMover _rigidbodyMover;
        private Pathfinder _pathfinder;
        private List<Vector2> _pathPoints;

        [Inject]
        private void Construct(Pathfinder pathfinder)
        {
            _pathfinder = pathfinder;
        }

        private void FixedUpdate()
        {
            UpdateVelocity();
        }

        public override void SetSpeed(float speed)
        {
            _rigidbodyMover.SetSpeed(speed);
        }
        
        public override float GetSpeed() => _rigidbodyMover.GetSpeed();


        public override void MoveTo(Vector2 position)
        {
            _pathPoints = _pathfinder.FindPath(transform.position, position);
            if(_pathPoints == null)
                Stop();
            else _rigidbodyMover.MoveByDirection(GetMoveDirection());
        }

        public override void Stop()
        {
            _rigidbodyMover.MoveByDirection(Vector2.zero);
            _pathPoints = null;
        }

        private void UpdateVelocity()
        {
            if (_pathPoints == null || _pathPoints.Count == 0) return;
            var direction = GetMoveDirection();
            if (direction.sqrMagnitude < 0.01f)
            {
                _pathPoints.RemoveAt(0);
                if (_pathPoints.Count > 0)
                {
                    direction = GetMoveDirection();
                    _rigidbodyMover.MoveByDirection(direction);
                }
                else
                    Stop();
            }
        }

        private Vector2 GetMoveDirection()
        {
            return _pathPoints[0] - (Vector2) transform.position;
        }
    }
}
