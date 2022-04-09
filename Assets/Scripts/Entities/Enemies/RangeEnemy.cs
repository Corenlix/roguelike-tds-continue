using Entities.Enemies.EnemyStateMachine;
using Entities.Enemies.EnemyStateMachine.Conditions;
using Entities.Enemies.EnemyStateMachine.States;
using UnityEngine;

namespace Entities.Enemies
{
    public class RangeEnemy : Enemy
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

        private StateMachine _stateMachine;

        private void Start()
        {
            InitStateMachine();
        }

        private void InitStateMachine()
        {
            var randomWalkState = new RandomWalkState(this, _randomWalkDistance, _randomWalkPeriod);
            var chaseShootState = new ChaseShootState(this, Target);
            var idleState = new IdleState(this, Target);
           
            randomWalkState.AddTransition(new Transition(chaseShootState, new InsideDistanceCondition(transform, Target, _walkToChaseDistance)));
            
            chaseShootState.AddTransition(new Transition(randomWalkState, new OutsideDistanceCondition(transform, Target, _chaseToWalkDistance)));
            chaseShootState.AddTransition(new Transition(idleState, new InsideDistanceCondition(transform, Target, _chaseToIdleDistance)));

            idleState.AddTransition(new Transition(chaseShootState, new OutsideDistanceCondition(transform, Target, _idleToChaseDistance)));
            
            _stateMachine = new StateMachine(randomWalkState);
        }
        
        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}