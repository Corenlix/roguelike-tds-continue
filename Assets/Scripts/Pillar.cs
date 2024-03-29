using System;
using Entities.Enemies;
using Infrastructure.Factory;
using LevelGeneration;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

public class Pillar : MonoBehaviour, IInteractable
{
    public event Action<Pillar, Enemy> BossSpawned;
    public event Action<IInteractable> Destroyed;

    public event Action<bool> Disappear;
    
    public Vector3 Position => transform.position;
    public bool NeedPressInteractButton => true;
    public string InteractText => "Summon boss";

    private EnemyId _id;
    private IGameFactory _gameFactory;
    private Level _level;
    
    public void Init(EnemyId id)
    {
        _id = id;
    }
    
    [Inject]
    private void Construct(IGameFactory gameFactory, Level level)
    {
        _gameFactory = gameFactory;
        _level = level;
    }
    
    public void Interact()
    {
        var boss = _gameFactory.CreateEnemy(_id, _level.GetFloorPoints()[300]);
        BossSpawned?.Invoke(this, boss);
        Disappear?.Invoke(true);
        Destroy(this);
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke(this);
        _level.SetFloorCell(transform.position);
    }
}
