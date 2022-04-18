using Entities.Enemies.EnemyStateMachine.States;

namespace Entities.Enemies.EnemyStateMachine
{
    public class StateMachine
    {
        private State _currentState;

        public StateMachine(State defaultState)
        {
            SetActiveState(defaultState);
        }

        public void Tick()
        {
            _currentState.Tick();
            if (_currentState.IsReadyToTransit(out State nextState))
            {
                SetActiveState(nextState);
            }
        }

        private void SetActiveState(State state)
        {
            if (state == null) return;
            _currentState?.Exit();
            _currentState?.Reset();
            _currentState = state;
            _currentState.Enter();
        }
    }
}