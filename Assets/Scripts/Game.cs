<<<<<<< Updated upstream
using Entities;
using Entities.Enemies;
=======
using System.Collections.Generic;
using System.Linq;
using Enemies;
>>>>>>> Stashed changes
using LevelGeneration;
using Pathfinding;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    public Level Level => _level;
    
    [SerializeField] private LevelGenerator _levelGenerator; 
    [SerializeField] private LevelDrawer _levelDrawer;
    [SerializeField] private Player _player;
    [SerializeField] private RangeEnemy _enemy;
    [SerializeField] private List<Chest> _chests;
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
        SpawnChest();
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

    private void SpawnChest()
    {
        List<Room> rooms = _level.MainRooms.ToList();
        foreach (var chest in _chests)
        {
            var roomToSpawn = rooms[Random.Range(1, rooms.Count)];
            float posX = Random.Range(roomToSpawn.Rect.xMin, roomToSpawn.Rect.xMax);
            float posY = Random.Range(roomToSpawn.Rect.yMin, roomToSpawn.Rect.xMax);
            
            Instantiate(chest, new Vector2(posX, posY), Quaternion.identity);
            rooms.Remove(roomToSpawn);
        }
    }   
}