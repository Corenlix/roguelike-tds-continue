using System.Collections.Generic;
using System.Linq;
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
        SpawnEnemies();
        SpawnChests();
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

    private void SpawnChests()
    {
        List<Room> rooms = _level.MainRooms.ToList();
        foreach (var chest in _chests)
        {
            var roomToSpawn = rooms[Random.Range(1, rooms.Count)];
            SpawnChest(roomToSpawn, chest);
            rooms.Remove(roomToSpawn);
        }
    }
    
    private void SpawnChest(Room spawnRoom, Chest chest)
    {
        float posX = Random.Range(spawnRoom.Rect.xMin, spawnRoom.Rect.xMax);
        float posY = Random.Range(spawnRoom.Rect.yMin, spawnRoom.Rect.yMax);
        
        Instantiate(chest, new Vector3(posX, posY), Quaternion.identity);
    }   
}