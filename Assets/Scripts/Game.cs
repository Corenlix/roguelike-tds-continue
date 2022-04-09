using Entities;
using Entities.Enemies;
using LevelGeneration;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    public Level Level => _level;
    
    [SerializeField] private LevelGenerator _levelGenerator; 
    [SerializeField] private LevelDrawer _levelDrawer;
    [SerializeField] private Player _player;
    [SerializeField] private RangeEnemy _enemy;
    private Level _level;
    private Pathfinder _pathfinder;

    private void Start()
    {
        _levelGenerator.LevelCreated += OnLevelGenerate;
        _levelGenerator.Generate();
    }

    private void OnLevelGenerate(Level level)
    {
        _level = level;
        _levelGenerator.LevelCreated -= OnLevelGenerate;
        _levelDrawer.DrawLevel(_level.LevelTable);
        _pathfinder = new Pathfinder(_level);
        SpawnPlayer();
        SpawnEnemy();
    }

    private void SpawnPlayer()
    {
        var roomToSpawn = _level.MainRooms[0];
        _player.transform.position = roomToSpawn.Rect.center;
    }

    private void SpawnEnemy()
    {
        var roomToSpawn = _level.MainRooms[1];
        var enemy = Instantiate(_enemy, roomToSpawn.Rect.center, Quaternion.identity);
        enemy.Init(_pathfinder, _player.transform);
    }
}