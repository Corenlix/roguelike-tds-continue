using System.Collections.Generic;
using LevelGeneration;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private LevelCreator _levelCreator; 
    [SerializeField] private LevelDrawer _levelDrawer;
    [SerializeField] private Player _player;
    private Level _level;
    
    private Level SpawnLevel()
    {
        var level = _levelCreator.CreateLevel();
        _levelDrawer.DrawLevel(level.LevelCells);
        return level;
    }

    private void SpawnPlayer()
    {
        List<Room> rooms = _level.Dungeon.Rooms;
        Room spawnRoom = rooms[0];
        _player.transform.position = spawnRoom.Rect.center;
    }
        
    private void Start()
    {
        _level = SpawnLevel();
        SpawnPlayer();
    }
}