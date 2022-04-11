using Infrastructure;
using LevelGeneration;
using UnityEngine;
using Zenject;

public class Game : MonoBehaviour
{
    private Level _level;
    private ILevelFactory _levelFactory;
    private IGameFactory _gameFactory;

    [Inject]
    private void Construct(ILevelFactory levelFactory, IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
        _levelFactory = levelFactory;
    }
    
    private void Start()
    {
        _levelFactory.Generate();
        _levelFactory.LevelGenerated += OnLevelGenerate;
    }

    private void OnLevelGenerate(Level level)
    {
        _level = level;
        _levelFactory.LevelGenerated -= OnLevelGenerate;
        InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        _gameFactory.CreatePlayer(_level.MainRooms[0].Rect.center);
        _gameFactory.CreatePlayerCamera();
        _gameFactory.CreateHud();
    }
}