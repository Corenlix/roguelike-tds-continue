using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.StaticData;
using Items;
using LevelGeneration;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

public class Chest : MonoBehaviour, IInteractable
{
    public event Action<IInteractable> Destroyed;
    public Vector3 Position => transform.position;
    public bool NeedPressInteractButton => true;
    public string InteractText => "Open Chest";
    
    private ChestStaticData _chestStaticData;
    private IGameFactory _gameFactory;
    private Level _level;

    [Inject]
    private void Construct(IGameFactory gameFactory, Level level)
    {
        _level = level;
        _gameFactory = gameFactory;
    }
    public void Init(ChestStaticData chestStaticData)
    {
        _chestStaticData = chestStaticData;
    }
    
    public void Interact()
    {
        _gameFactory.CreateItemsForLoot(_chestStaticData.LootId, transform.position);
        Destroy(gameObject);
    }
    
    private void OnDestroy()
    {
        Destroyed?.Invoke(this);
        _level.SetFloorCell(transform.position);
    }
}
