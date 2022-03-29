namespace Enemies.EnemyStateMachine.States
{
    public abstract class State
    {
        private Transition[] _transitions;

        public void SetTransitions(params Transition[] transitions)
        {
            _transitions = transitions;
        }

        public bool IsReadyToTransit(out State nextState)
        {
            foreach (var transition in _transitions)
            {
                if (transition.IsReadyToTransit(out nextState))
                    return true;
            }

            nextState = null;
            return false;
        }
    
        public abstract void Enter();
        public abstract void Tick();
        public abstract void Exit();
    }
}
