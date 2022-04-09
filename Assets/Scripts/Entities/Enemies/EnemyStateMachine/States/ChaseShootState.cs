using UnityEngine;

namespace Entities.Enemies.EnemyStateMachine.States
{
    public class ChaseShootState : State
    {
        private readonly RangeEnemy _enemy;
        private readonly Transform _target;

        public  ChaseShootState(RangeEnemy enemy, Transform target)
        {
            _enemy = enemy;
            _target = target;
        }

        public override void Enter()
        {
            
        }

        public override void Tick()
        {
            _enemy.MoveTo(_target.position);
            _enemy.Attack(_target.position);
        }

        public override void Exit()
        {
            _enemy.Stop();
        }
    }
}

