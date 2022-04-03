using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class PathfindMover : RigidbodyMover
{
    private Pathfinder _pathfinder;
    private List<Vector2Int> _pathPoints;

    public void Init(Pathfinder pathfinder)
    {
        _pathfinder = pathfinder;
    }
    
    private void Update()
    {
        UpdateVelocity();
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
        if (direction.sqrMagnitude < 0.1f)
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