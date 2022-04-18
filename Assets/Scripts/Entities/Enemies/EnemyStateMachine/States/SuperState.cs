namespace Entities.Enemies.EnemyStateMachine.States
{
    public class SuperState : State
    {
        private readonly State[] _states;
        
        public SuperState(params State[] states)
        {
            _states = states;
        }
        
        public override void Enter()
        {
            foreach (var state in _states)
            {
                state.Enter();
            }
        }

        public override void Tick()
        {
            foreach (var state in _states)
            {
                state.Tick();
            }
        }

        public override void Exit()
        {
            foreach (var state in _states)
            {
                state.Exit();
            }
        }
    }
}