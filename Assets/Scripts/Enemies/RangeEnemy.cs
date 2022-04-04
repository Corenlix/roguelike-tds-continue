using System.Data;
using Enemies.EnemyStateMachine;
using Enemies.EnemyStateMachine.Conditions;
using Enemies.EnemyStateMachine.States;
using Pathfinding;
using UnityEngine;
using Weapons;

namespace Enemies
{
    public class RangeEnemy : MonoBehaviour
    {
        [Header("Random Walk State")]
        [SerializeField] private int _randomWalkDistance;
        [SerializeField] private float _randomWalkPeriod;

        [Header("Chase State")] 
        [SerializeField] private float _enterChaseStateDistance;
        [SerializeField] private float _chaseMinPeriod;
        [SerializeField] private float _chaseMaxPeriod;
        [SerializeField] private float _exitChaseStateDistance;
        
        [SerializeField] private PathfindMover _pathfindMover;
        [SerializeField] private EntityView _view;
        [SerializeField] private Weapon _weapon;

        private StateMachine _stateMachine;
        private Transform _target;

        public void Init(Pathfinder pathfinder, Transform target)
        {
            _pathfindMover.Init(pathfinder);
            SetTarget(target);
        }
        
        public void SetTarget(Transform target)
        {
            _target = target;
        }
        
        private void Start()
        {
            InitStateMachine();
        }

        private void InitStateMachine()
        {
            var randomWalkState = new RandomWalkState(_view, transform, _pathfindMover, _randomWalkDistance, _randomWalkPeriod);
            var chaseState = new ChaseTargetState(transform, _view, _weapon, _target, _pathfindMover, _chaseMinPeriod, _chaseMaxPeriod);
            
            randomWalkState.SetTransitions(new Transition(chaseState, new InsideDistanceCondition(transform, _target, _enterChaseStateDistance)));
            chaseState.SetTransitions(new Transition(randomWalkState, new OutsideDistanceCondition(transform, _target, _exitChaseStateDistance)));

            _stateMachine = new StateMachine(randomWalkState);
        }
        
        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}