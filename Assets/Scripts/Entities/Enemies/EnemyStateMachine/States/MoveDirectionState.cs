
using UnityEngine;

namespace Entities.Enemies.EnemyStateMachine.States
{
    public class MoveDirectionState : State
    {
        private readonly Transform _target;
        private readonly EnemyView _enemyView;
        private readonly Mover _mover;
        private  Vector3 _moveDirection;

        public MoveDirectionState(EnemyView enemyView, Mover mover, Transform target)
        {
            _mover = mover;
            _enemyView = enemyView;
            _target = target;
        }
        public override void Enter()
        {
            _moveDirection = _target.position - _mover.transform.position;
        }

        public override void Tick()
        {
            Vector2 destination = _mover.transform.position + _moveDirection;
            _mover.MoveTo(destination);
            _enemyView.LookTo(destination);
        }

        public override void Exit()
        {
            _mover.Stop();
        }
    }
}