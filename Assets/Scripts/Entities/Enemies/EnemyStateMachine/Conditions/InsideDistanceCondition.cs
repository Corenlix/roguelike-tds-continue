using UnityEngine;

namespace Entities.Enemies.EnemyStateMachine.Conditions
{
    public class InsideDistanceCondition : Condition
    {
        private readonly Transform _transform;
        private readonly Transform _targetTransform;
        private readonly float _maxDistanceSquare;
    
        public InsideDistanceCondition(Transform transform, Transform targetTransform, float maxDistance)
        {
            _transform = transform;
            _targetTransform = targetTransform;
            _maxDistanceSquare = maxDistance*maxDistance;
        }
        
        public override bool IsConditionMet() => Vector2.SqrMagnitude(_transform.position - _targetTransform.position) <= _maxDistanceSquare;
        public override void Reset()
        {
            
        }
    }
}