using UnityEngine;

namespace Entities.Enemies.EnemyStateMachine.Conditions
{
    public class WaitAnimationEndCondition : Condition
    {
        private readonly EnemyView _view;
        private readonly AnimationNames _animation;
        private bool _ended;

        public WaitAnimationEndCondition(EnemyView view, AnimationNames animation)
        {
            _animation = animation;
            _view = view;
            _view.StateExited += OnAnimationEnd;
        }

        private void OnAnimationEnd(AnimatorStateInfo obj)
        {
            if (!obj.IsName(_animation.ToString())) return;
            _ended = true;
            _view.StateExited -= OnAnimationEnd;
        }

        public override bool IsConditionMet()
        {
            return _ended;
        }

        public override void Reset()
        {
            _ended = false;
            _view.StateExited -= OnAnimationEnd;
            _view.StateExited += OnAnimationEnd;
        }
    }
}