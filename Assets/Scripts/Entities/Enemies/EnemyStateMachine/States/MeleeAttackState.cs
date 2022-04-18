using Entities.Enemies.EnemyWeapons;
using UnityEngine;

namespace Entities.Enemies.EnemyStateMachine.States
{
    public class MeleeAttackState : State
    {
        private readonly EnemyView _view;
        private readonly Splash _weapon;

        public MeleeAttackState(EnemyView view, Splash weapon)
        {
            _weapon = weapon;
            _view = view;
        }
        
        public override void Enter()
        {
            _view.Attack();
            _view.StateExited += OnAnimationEnd;
        }

        public override void Tick()
        {
            
        }

        private void OnAnimationEnd(AnimatorStateInfo state)
        {
            if (state.IsName(AnimationNames.Attack.ToString()))
            {
                _weapon.TryHit();
            }
        }

        public override void Exit()
        {
            _view.StateExited -= OnAnimationEnd;
        }
    }
}