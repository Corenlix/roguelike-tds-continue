﻿using Weapons;
using UnityEngine;

namespace Enemies.EnemyStateMachine.States
{
    public class IdleState : State
    {
        private readonly Transform _target;
        private readonly EntityView _entityView;
        private Weapon _weapon;
        
        public IdleState(EntityView entityView, Weapon weapon, Transform target)
        {
            _target = target;
            _entityView = entityView;
            _weapon = weapon;
        }
        
        public override void Enter()
        {
            
        }

        public override void Tick()
        {
            Attack();
        }

        public override void Exit()
        {
            
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