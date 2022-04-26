using Infrastructure.SaveLoad;
using UnityEngine.SceneManagement;

namespace GameState
{
    public class LooseState : IState
    {
        private GameStateMachine _gameStateMachine;
        private ISaveLoadService _saveLoadService;

        public LooseState(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            _saveLoadService.Clear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Exit()
        {
            
        }
    }
}