using System;
using UnityEngine;

namespace Enemies.EnemyStateMachine.Conditions
{
    public class InsideDistanceCondition : Condition
    {
        private readonly Func<Vector2> _positionFunc;
        private readonly Func<Vector2> _targetPositionFunc;
        private readonly float _maxDistanceSquare;
    
        public InsideDistanceCondition(Func<Vector2> positionFunc, Func<Vector2> targetPositionFunc, float maxDistance)
        {
            _targetPositionFunc = targetPositionFunc;
            _positionFunc = positionFunc;
            _maxDistanceSquare = maxDistance*maxDistance;
        }
        
        public override bool IsConditionMet() => Vector2.SqrMagnitude(_positionFunc() - _targetPositionFunc()) <= _maxDistanceSquare;
    }
}