namespace Entities.Enemies.EnemyStateMachine.States
{
    public class PrepareAttackState : State
    {
        private readonly EnemyView _view;

        public PrepareAttackState(EnemyView view)
        {
            _view = view;
        }

        public override void Enter()
        {
            _view.PrepareAttack();
        }

        public override void Tick()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}