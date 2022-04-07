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

        [Header("Idle State")] 
        [SerializeField] private int _chaseToIdleDistance;
        [SerializeField] private int _idleToChaseDistance;
        
        [Header("Chase and Shoot State")]
        [SerializeField] private int _walkToChaseDistance;
        [SerializeField] private int _chaseToWalkDistance;
        
        [SerializeField] private PathfindMover _pathfindMover;
        [SerializeField] private EntityView _view;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Health _health;
        
        private StateMachine _stateMachine;
        private Transform _target;

        public void Init(Pathfinder pathfinder, Transform target)
        {
            _health.Died += OnDied;
            _pathfindMover.Init(pathfinder);
            SetTarget(target);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void Attack(Vector3 target)
        {
            _view.LookTo(target);
            _weapon.AimTo(target);
            _weapon.TryShoot();
        }

        public void MoveTo(Vector3 destination) => _pathfindMover.SetMovePoint(_target.position);

        public void Stop() => _pathfindMover.Reset();

        public void OnDied()
        {
            _health.Died -= OnDied;
            Destroy(gameObject);
        }
        
        private void Start()
        {
            InitStateMachine();
        }

        private void InitStateMachine()
        {
            var randomWalkState = new RandomWalkState(this, _randomWalkDistance, _randomWalkPeriod);
            var chaseShootState = new ChaseShootState(this, _target);
            var idleState = new IdleState(this, _target);
           
            randomWalkState.AddTransition(new Transition(chaseShootState, new InsideDistanceCondition(transform, _target, _walkToChaseDistance)));
            
            chaseShootState.AddTransition(new Transition(randomWalkState, new OutsideDistanceCondition(transform, _target, _chaseToWalkDistance)));
            chaseShootState.AddTransition(new Transition(idleState, new InsideDistanceCondition(transform, _target, _chaseToIdleDistance)));

            idleState.AddTransition(new Transition(chaseShootState, new OutsideDistanceCondition(transform, _target, _idleToChaseDistance)));
            
            _stateMachine = new StateMachine(randomWalkState);
        }
        
        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}