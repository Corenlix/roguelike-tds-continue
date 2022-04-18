using Entities.Enemies.EnemyWeapons;
using UnityEngine;

namespace Entities.Enemies.EnemyStateMachine.States
{
    public class ShootState : State
    {
        private readonly Transform _target;
        private readonly EnemyShootWeapon _weapon;
        private readonly EnemyView _enemyView;

        public ShootState(EnemyView enemyView, EnemyShootWeapon weapon, Transform target)
        {
            _enemyView = enemyView;
            _weapon = weapon;
            _target = target;
        }
        
        public override void Enter()
        {
            
        }

        public override void Tick()
        {
            _enemyView.LookTo(_target.position);
            _weapon.AimTo(_target.position);
            _weapon.TryHit();
        }

        public override void Exit()
        {
        
        }
    }
}
