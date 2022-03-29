using System.Collections.Generic;
using Enemies;
using LevelGeneration;
using Pathfinding;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Level Level => _level;
    
    [SerializeField] private LevelCreator _levelCreator; 
    [SerializeField] private LevelDrawer _levelDrawer;
    [SerializeField] private Player _player;
    [SerializeField] private RangeEnemy _enemy;
    private Level _level;
    private Pathfinder _pathfinder;
    
    private void Start()
    {
        SpawnLevel();
        _pathfinder = new Pathfinder(_level);
        SpawnPlayer();
        SpawnEnemy();
    }
    
    private void SpawnLevel()
    {
        _level = _levelCreator.CreateLevel();
        _levelDrawer.DrawLevel(_level.LevelTable);
    }

    private void SpawnPlayer()
    {
        List<Room> rooms = _level.Dungeon.Rooms;
        Room spawnRoom = rooms[0];
        _player.transform.position = spawnRoom.Rect.center;
    }

    private void SpawnEnemy()
    {
        List<Room> rooms = _level.Dungeon.Rooms;
        Room spawnRoom = rooms[1];
        var enemy = Instantiate(_enemy, spawnRoom.Rect.center, Quaternion.identity);
        enemy.Init(_pathfinder, _player.transform);
    }
}