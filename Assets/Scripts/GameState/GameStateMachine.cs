using System;
using System.Collections.Generic;
using Infrastructure;

namespace GameState
{
    public class GameStateMachine
    {
        private Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(ILevelFactory levelFactory, IGameFactory gameFactory)
        {
            _states = new Dictionary<Type, IState>()
            {
                {typeof(GenerateLevelState), new GenerateLevelState(this, levelFactory, gameFactory)}
            };

            Enter<GenerateLevelState>();
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            _activeState?.Exit();
      
            TState state = GetState<TState>();
            _activeState = state;
      
            return state;
        }

        private TState GetState<TState>() where TState : class, IState => 
            _states[typeof(TState)] as TState;
    }
}
