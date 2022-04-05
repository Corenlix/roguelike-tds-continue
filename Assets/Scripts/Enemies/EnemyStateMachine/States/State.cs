using System.Collections.Generic;
 
 namespace Enemies.EnemyStateMachine.States
{
    public abstract class State
    {
        private List<Transition> _transitions = new List<Transition>();

        public void SetTransitions(List<Transition> transitions)
        {
            _transitions = transitions;
        }

        public void AddTransition(Transition transition)
        {
            _transitions.Add(transition);
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
