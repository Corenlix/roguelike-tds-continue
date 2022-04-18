using UnityEngine;

namespace Entities.Enemies.EnemyStateMachine.States
{
    public class ChaseState : State
    {
        private readonly Transform _target;
        private readonly EnemyView _enemyView;
        private readonly Mover _mover;

        public ChaseState(EnemyView enemyView, Mover mover, Transform target)
        {
            _mover = mover;
            _enemyView = enemyView;
            _target = target;
        }
        public override void Enter()
        {
        
        }

        public override void Tick()
        {
            var position = _target.position;
            _mover.MoveTo(position);
            _enemyView.LookTo(position);
        }

        public override void Exit()
        {
            _mover.Stop();
        }
    }
}
