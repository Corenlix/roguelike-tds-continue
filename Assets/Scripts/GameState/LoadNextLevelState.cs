using Infrastructure.Progress;
using Infrastructure.SaveLoad;
using LevelGeneration;
using UnityEngine.SceneManagement;

namespace GameState
{
    public class LoadNextLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISaveLoadService _saveLoadService;
        private IProgressService _progressService;

        public LoadNextLevelState(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService, IProgressService progressService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            LevelId currentLevelId = _saveLoadService.GetValue(SaveLoadKey.Level, LevelId.FirstLevel);
            LevelId nextLevelId = currentLevelId + 1;
            _saveLoadService.SetValue(nextLevelId, SaveLoadKey.Level);
            _progressService.Save();
            _saveLoadService.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Exit()
        {
            
        }
    }
}