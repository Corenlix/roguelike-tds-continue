using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.StaticData;
using Items;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

public class Chest : MonoBehaviour, IInteractable
{
    public Vector3 Position => transform.position;
    public bool NeedPressInteractButton => true;
    public string InteractText => "Open Chest";
    
    private ChestStaticData _chestStaticData;
    private IStaticDataService _staticDataService;
    private IGameFactory _gameFactory;

    [Inject]
    private void Construct(IStaticDataService staticDataService, IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
        _staticDataService = staticDataService;
    }
    public void Init(ChestStaticData chestStaticData)
    {
        _chestStaticData = chestStaticData;
    }
    
    public void Interact()
    {
        var loot = _staticDataService.ForLoot(_chestStaticData.LootId);
        var itemId = loot.Get();
        _gameFactory.CreateItem(itemId, transform.position);
        Destroy(gameObject);
    }
}
