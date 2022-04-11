using System.Collections.Generic;
using System.Linq;
using Entities;
using Entities.Enemies;
using Infrastructure;
using LevelGeneration;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Game : MonoBehaviour
{
    public Level Level => _level;
    [SerializeField] private RangeEnemy _enemy;
    [SerializeField] private List<Chest> _chests;
    private LevelGenerator _levelGenerator;
    private Level _level;
    private Pathfinder _pathfinder;
    
    private void Start()
    {
        _levelGenerator = Instantiate(Resources.Load<LevelGenerator>("LevelGenerator"));
        _levelGenerator.LevelCreated += OnLevelGenerate;
        _levelGenerator.Generate();
    }

    private void OnLevelGenerate(Level level)
    {
        _level = level;
        _levelGenerator.LevelCreated -= OnLevelGenerate;
        _pathfinder = new Pathfinder(_level);
        SpawnChests();
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