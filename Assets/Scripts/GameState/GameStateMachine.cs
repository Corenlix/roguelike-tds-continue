using System;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Factory;
using Infrastructure.Progress;
using Infrastructure.SaveLoad;
using Infrastructure.StaticData;

namespace GameState
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(IGameFactory gameFactory, IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _states = new Dictionary<Type, IState>()
            {
                {typeof(LoadLevelState), new LoadLevelState(this, gameFactory, progressService, saveLoadService)},
                {typeof(LoadNextLevelState), new LoadNextLevelState(this, saveLoadService, progressService)},
                {typeof(LooseState), new LooseState(this, saveLoadService)}
            };

            Enter<LoadLevelState>();
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
