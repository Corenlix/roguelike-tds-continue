using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Enemies.EnemyStateMachine.States
{
    public class ChaseShootState : State
    {
        private readonly PathfindMover _pathfindMover;
        private readonly Transform _target;
        private readonly EntityView _entityView;
        private Weapon _weapon;

        public  ChaseShootState(EntityView entityView, Weapon weapon, Transform target, PathfindMover pathfindMover)
        {
            _pathfindMover = pathfindMover;
            _target = target;
            _entityView = entityView;
            _weapon = weapon;
        }

        public override void Enter()
        {
            
        }

        public override void Tick()
        {
            MoveToTarget();
            Attack();
        }

        public override void Exit()
        {
            _pathfindMover.Reset();
        }

        private void MoveToTarget()
        {
            _pathfindMover.SetMovePoint(_target.position);
        }
        
        private void Attack()
        {
            var target = _target.position;
            _entityView.LookTo(target);
            _weapon.AimTo(target);
            _weapon.TryShoot();
        }
    }
}

