using Weapons;
using UnityEngine;

namespace Enemies.EnemyStateMachine.States
{
    public class IdleState : State
    {
        private readonly Transform _target;
        private readonly RangeEnemy _enemy;
        
        public IdleState(RangeEnemy enemy, Transform target)
        {
            _enemy = enemy;
            _target = target;
        }
        
        public override void Enter()
        {
            _enemy.MoveTo(_enemy.transform.position);
        }

        public override void Tick()
        {
            _enemy.Attack(_target.position);
        }

        public override void Exit()
        {
            
        }

    }
}