using System;
using Entities.Enemies.EnemyStateMachine;
using Entities.Enemies.EnemyStateMachine.Conditions;
using Entities.Enemies.EnemyStateMachine.States;
using Entities.Enemies.StaticData;
using UnityEngine;

namespace Entities.Enemies
{
    public class MeleeEnemy : Enemy
    {
        private StateMachine _stateMachine;
        private protected MeleeEnemyStaticData Data;

        protected override void OnInit(EnemyStaticData staticData)
        {
            Data = (MeleeEnemyStaticData)staticData;
            InitStateMachine();
        }

        private void InitStateMachine()
        {
            var randomWalkState = new RandomWalkState(this, Data.RandomWalkDistance, Data.RandomWalkPeriod);
            var chaseState = new ChaseState(this, Target);
            var attackState = new AttackState(this, Target);

            randomWalkState.AddTransition(new Transition(chaseState, new InsideDistanceCondition(transform, Target, Data.WalkToChaseDistance)));
            
            chaseState.AddTransition(new Transition(randomWalkState, new OutsideDistanceCondition(transform, Target, Data.ChaseToWalkDistance)));
            chaseState.AddTransition(new Transition(attackState, new InsideDistanceCondition(transform, Target, Data.ChaseToAttackDistance)));

            attackState.AddTransition(new Transition(chaseState, new InsideDistanceCondition(transform, Target, Data.AttackToChaseDistance)));

            _stateMachine = new StateMachine(randomWalkState);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }
        
        public void Attack() => _view.PlayAttack();
    }
}
