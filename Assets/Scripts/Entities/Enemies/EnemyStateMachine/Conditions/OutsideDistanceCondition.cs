using UnityEngine;

namespace Entities.Enemies.EnemyStateMachine.Conditions
{
    public class OutsideDistanceCondition : Condition
    {
        private readonly Transform _transform;
        private readonly Transform _targetTransform;
        private readonly float _minDistanceSquare;
    
        public OutsideDistanceCondition(Transform transform, Transform targetTransform, float minDistance)
        {
            _transform = transform;
            _targetTransform = targetTransform;
            _minDistanceSquare = minDistance*minDistance;
        }
        public override bool IsConditionMet() => Vector2.SqrMagnitude(_transform.position - _targetTransform.position) > _minDistanceSquare;
        public override void Reset()
        {
            
        }
    }
}