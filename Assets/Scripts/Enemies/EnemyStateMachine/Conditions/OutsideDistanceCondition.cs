using System;
using UnityEngine;

namespace Enemies.EnemyStateMachine.Conditions
{
    public class OutsideDistanceCondition : Condition
    {
        private readonly Func<Vector2> _positionFunc;
        private readonly Func<Vector2> _targetPositionFunc;
        private readonly float _minDistanceSquare;
    
        public OutsideDistanceCondition(Func<Vector2> positionFunc, Func<Vector2> targetPositionFunc, float minDistance)
        {
            _targetPositionFunc = targetPositionFunc;
            _positionFunc = positionFunc;
            _minDistanceSquare = minDistance*minDistance;
        }
        public override bool IsConditionMet() => Vector2.SqrMagnitude(_positionFunc() - _targetPositionFunc()) > _minDistanceSquare;
    }
}