using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace Entities
{
    public class PathfindMover : MonoBehaviour
    {
        [SerializeField] private RigidbodyMover _rigidbodyMover;
        private Pathfinder _pathfinder;
        private List<Vector2> _pathPoints;

        public void Init(Pathfinder pathfinder, float speed)
        {
            _rigidbodyMover.Init(speed);
            _pathfinder = pathfinder;
        }
    
        private void FixedUpdate()
        {
            UpdateVelocity();
        }

        public void SetMovePoint(Vector2 position)
        {
            _pathPoints = _pathfinder.FindPath(transform.position, position);
            if(_pathPoints == null)
                Reset();
            else _rigidbodyMover.MoveByDirection(GetMoveDirection());
        }

        public void Reset()
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
                    Reset();
            }
        }

        private Vector2 GetMoveDirection()
        {
            return _pathPoints[0] - (Vector2) transform.position;
        }
    }
}
