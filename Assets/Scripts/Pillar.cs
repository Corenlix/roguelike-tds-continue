using System;
using System.Collections;
using System.Collections.Generic;
using Entities.Enemies;
using Infrastructure.Factory;
using LevelGeneration;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;

public class Pillar : MonoBehaviour, IInteractable
{
    public Vector3 Position => transform.position;
    public bool NeedPressInteractButton => true;
    public string InteractText => "Summon boss";

    private EnemyId _id;
    private PillarView _pillarView;
    private IGameFactory _gameFactory;
    private Level _level;

    private bool _isUsed;

    private void Awake()
    {
        _pillarView = GetComponent<PillarView>();
    }
    
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
        if (_isUsed) return;
        _pillarView.BrokePillar();
        _gameFactory.CreateEnemy(_id, _level.GetFloorPoints()[300]);
        _isUsed = true;
    }
}
