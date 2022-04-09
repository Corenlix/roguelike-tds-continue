using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace Entities
{
    public class PathfindMover : RigidbodyMover
    {
        private Pathfinder _pathfinder;
        private List<Vector2> _pathPoints;

        public void Init(Pathfinder pathfinder)
        {
            _pathfinder = pathfinder;
        }
    
        protected override void FixedUpdate()
        {
            UpdateVelocity();
            base.FixedUpdate();
        }

        public void SetMovePoint(Vector2 position)
        {
            _pathPoints = _pathfinder.FindPath(transform.position, position);
            if(_pathPoints == null)
                Reset();
            else MoveByDirection(GetMoveDirection());
        }

        public void Reset()
        {
            MoveByDirection(Vector2.zero);
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
                    MoveByDirection(direction);
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
