using UnityEngine;

namespace Entities.Enemies.EnemyStateMachine.States
{
    public class AttackState : State
    {
        private readonly MeleeEnemy _meleeEnemy;
        private readonly Transform _target;

        public AttackState(MeleeEnemy meleeEnemy, Transform target)
        {
            _meleeEnemy = meleeEnemy;
            _target = target;
        }
        public override void Enter()
        {
            _meleeEnemy.Attack();
        }

        public override void Tick()
        {
            _meleeEnemy.MoveTo(_target.position);
        }

        public override void Exit()
        {
        
        }
    }
}
