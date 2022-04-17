using UnityEngine;

namespace Entities.Enemies
{
    public class EnemyView : EntityView
    {
        private static readonly int Attack = Animator.StringToHash("Attack");
                
        public void PlayAttack()
        {
            _animator.SetTrigger(Attack);
        }
    }
}
