using System.Data;
using Enemies.EnemyStateMachine;
using Enemies.EnemyStateMachine.Conditions;
using Enemies.EnemyStateMachine.States;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

namespace Enemies
{
    public class RangeEnemy : MonoBehaviour
    {
        [Header("Random Walk State")]
        [SerializeField] private int _randomWalkDistance;
        [SerializeField] private float _randomWalkPeriod;

        [Header("Idle State")] 
        [SerializeField] private int _chaseToIdleDistance;
        [SerializeField] private int _idleToChaseDistance;
        
        [Header("Chase and Shoot State")]
        [SerializeField] private int _walkToChaseDistance;
        [SerializeField] private int _chaseToWalkDistance;
        
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
            var chaseShootState = new ChaseShootState(_view, _weapon, _target, _pathfindMover);
            var idleState = new IdleState(_view, _weapon, _target);
           
            randomWalkState.AddTransition(new Transition(chaseShootState, new InsideDistanceCondition(transform, _target, _walkToChaseDistance)));
            randomWalkState.AddTransition(new Transition(idleState, new InsideDistanceCondition(transform, _target, _chaseToIdleDistance)));
            
            chaseShootState.AddTransition(new Transition(randomWalkState, new OutsideDistanceCondition(transform, _target, _chaseToWalkDistance)));
            chaseShootState.AddTransition(new Transition(idleState, new InsideDistanceCondition(transform, _target, _chaseToIdleDistance)));

            idleState.AddTransition(new Transition(chaseShootState, new InsideDistanceCondition(transform, _target, _idleToChaseDistance)));
            
            _stateMachine = new StateMachine(randomWalkState);
        }
        
        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}