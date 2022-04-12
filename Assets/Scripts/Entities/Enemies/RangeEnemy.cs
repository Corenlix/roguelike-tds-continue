using Entities.Enemies.EnemyStateMachine;
using Entities.Enemies.EnemyStateMachine.Conditions;
using Entities.Enemies.EnemyStateMachine.States;
using Infrastructure;
using UnityEngine;

namespace Entities.Enemies
{
    public class RangeEnemy : Enemy
    {
        private StateMachine _stateMachine;
        private RangeEnemyStaticData _data;
        
        public override void Init(EnemyStaticData data)
        {
            _data = (RangeEnemyStaticData)data;
        }

        private void Start()
        {
            InitStateMachine();
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void InitStateMachine()
        {
            var randomWalkState = new RandomWalkState(this, _data.RandomWalkDistance, _data.RandomWalkPeriod);
            var chaseShootState = new ChaseShootState(this, Target);
            var idleState = new IdleState(this, Target);
           
            randomWalkState.AddTransition(new Transition(chaseShootState, new InsideDistanceCondition(transform, Target, _data.WalkToChaseDistance)));
            
            chaseShootState.AddTransition(new Transition(randomWalkState, new OutsideDistanceCondition(transform, Target, _data.ChaseToWalkDistance)));
            chaseShootState.AddTransition(new Transition(idleState, new InsideDistanceCondition(transform, Target, _data.ChaseToIdleDistance)));

            idleState.AddTransition(new Transition(chaseShootState, new OutsideDistanceCondition(transform, Target, _data.IdleToChaseDistance)));
            
            _stateMachine = new StateMachine(randomWalkState);
        }
    }
}