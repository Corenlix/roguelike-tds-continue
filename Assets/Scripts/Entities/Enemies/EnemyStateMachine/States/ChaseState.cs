using UnityEngine;

namespace Entities.Enemies.EnemyStateMachine.States
{
    public class ChaseState : State
    {
        private readonly MeleeEnemy _meleeEnemy;
        private readonly Transform _target;

        public ChaseState(MeleeEnemy meleeEnemy, Transform target)
        {
            _meleeEnemy = meleeEnemy;
            _target = target;
        }
        public override void Enter()
        {
        
        }

        public override void Tick()
        {
            var position = _target.position;
            _meleeEnemy.MoveTo(position);
            _meleeEnemy.LookToTarget(position);
        }

        public override void Exit()
        {
            _meleeEnemy.Stop();
        }
    }
}
