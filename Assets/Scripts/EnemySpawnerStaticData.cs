using Entities.Enemies;
using Items;
using UnityEngine;

[CreateAssetMenu(menuName = "Static Data/EnemiesSpawner", fileName = "EnemySpawnerStaticData")]
public class EnemySpawnerStaticData : ScriptableObject
{
    public EnemySpawnerId Id;
    public WeightsRandom<EnemyId> Enemies;
    public float SpawnPeriod;
}
