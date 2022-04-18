using UnityEngine;

namespace Entities.Enemies
{
    public class EnemyView : EntityView
    {
        private static readonly int AttackState = Animator.StringToHash("Attack");
        private static readonly int PrepareAttackState = Animator.StringToHash("PrepareAttack");

        public void PrepareAttack()
        {
            _animator.SetTrigger(PrepareAttackState);
        }

        public void Attack()
        {
            _animator.SetTrigger(AttackState);
        }
    }
}
