using System.Collections.Generic;

namespace Entities.Enemies.EnemyStateMachine.States
{
    public abstract class State
    {
        private readonly List<Transition> _transitions = new List<Transition>();

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

        public void Reset()
        {
            _transitions.ForEach(x=>x.Reset());
        }
    }
}