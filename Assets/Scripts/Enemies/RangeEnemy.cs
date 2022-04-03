using System.Data;
using Enemies.EnemyStateMachine;
using Enemies.EnemyStateMachine.Conditions;
using Enemies.EnemyStateMachine.States;
using Pathfinding;
using UnityEngine;

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

        private EnemyStateMachine.EnemyStateMachine _stateMachine;
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
            var farCondition = new OutsideDistanceCondition(() => transform.position,
                () => _target.position, _exitChaseStateDistance);
            var nearCondition = new InsideDistanceCondition(() => transform.position,
                () => FindObjectOfType<Player>().transform.position, _enterChaseStateDistance);
            
            var randomWalkState = new RandomWalkState(_view, transform, _pathfindMover, _randomWalkDistance, _randomWalkPeriod);
            var chaseState = new ChaseTargetState(transform, _view, () => _target.position, _pathfindMover, _chaseMinPeriod, _chaseMaxPeriod);

            randomWalkState.SetTransitions(new Transition(chaseState, nearCondition));
            chaseState.SetTransitions(new Transition(randomWalkState, farCondition));

            _stateMachine = new EnemyStateMachine.EnemyStateMachine(randomWalkState);
        }
        
        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}