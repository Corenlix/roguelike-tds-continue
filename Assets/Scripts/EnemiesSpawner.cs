using Entities.Enemies;
using Infrastructure.Factory;
using LevelGeneration;
using UnityEngine;
using Zenject;

public class EnemiesSpawner : MonoBehaviour
{
    private IGameFactory _gameFactory;
    private Level _level;
    private EnemySpawnerStaticData _staticData;
    private float _timeToSpawn;


    [Inject]
    private void Construct(IGameFactory gameFactory, Level level)
    {
        _gameFactory = gameFactory;
        _level = level;
    }

    public void Init(EnemySpawnerStaticData staticData)
    {
        _staticData = staticData;
        _timeToSpawn = staticData.SpawnPeriod;
    }

    private void Update()
    {
        _timeToSpawn -= Time.deltaTime;
        if (_timeToSpawn <= 0)
        {
            var spawnCell = _level.GetFloorPoints()[Random.Range(0, _level.GetFloorPoints().Count)];
            _gameFactory.CreateEnemy(_staticData.Enemies.Get(EnemyId.None), spawnCell);
            _timeToSpawn = _staticData.SpawnPeriod;
        }
    }
}