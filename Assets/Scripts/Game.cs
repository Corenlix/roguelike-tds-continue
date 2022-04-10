using Entities;
using Entities.Enemies;
using LevelGeneration;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

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
        SpawnEnemies();
    }

    private void SpawnPlayer()
    {
        var roomToSpawn = _level.MainRooms[0];
        _player.transform.position = roomToSpawn.Rect.center;
    }

    private void SpawnEnemies()
    {
        for(int i = 0; i < 22; i++)
            SpawnEnemy(_level.MainRooms[Random.Range(2, _level.MainRooms.Count)]);
    }
    
    private void SpawnEnemy(Room spawnRoom)
    {
        var position = spawnRoom.Rect.center;
        var enemy = Instantiate(_enemy, position, Quaternion.identity);
        enemy.Init(_pathfinder, _player.transform);
    }

    private void OnDrawGizmos()
    {
        if (_level == null)
            return;
        foreach (var levelMainRoom in _level.MainRooms)
        {
            Gizmos.DrawCube(levelMainRoom.Rect.center, new Vector3(levelMainRoom.Rect.width, levelMainRoom.Rect.height));
        }
    }
}