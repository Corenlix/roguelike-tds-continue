using System.Linq;
using Entities.Enemies.EnemyStateMachine.Conditions;
using Entities.Enemies.EnemyStateMachine.States;

namespace Entities.Enemies.EnemyStateMachine
{
    public class Transition
    {
        private readonly Condition[] _conditions;
        private readonly State _nextState;

        public Transition(State nextState, params Condition[] conditions)
        {
            _conditions = conditions;
            _nextState = nextState;
        }

        public bool IsReadyToTransit(out State nextState)
        {
            bool isReady = _conditions.All(x => x.IsConditionMet());
            nextState = isReady ? _nextState : null;
            return isReady;
        }
    }
}