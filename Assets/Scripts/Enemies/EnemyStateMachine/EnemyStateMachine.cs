using Enemies.EnemyStateMachine.States;

namespace Enemies.EnemyStateMachine
{
    public class EnemyStateMachine
    {
        private State _currentState;

        public EnemyStateMachine(State defaultState)
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
            _currentState = state;
            _currentState.Enter();
        }
    }
}