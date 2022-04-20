using Entities.Enemies.EnemyStateMachine;
using Entities.Enemies.EnemyStateMachine.Conditions;
using Entities.Enemies.EnemyStateMachine.States;
using Entities.Enemies.StaticData;
using UnityEngine;

namespace Entities.Enemies
{
    public class EyeBoss : Enemy
    {
        [SerializeField] private HitByTrigger _hitByTrigger;
        [SerializeField] private EyeView _view;
        [SerializeField] private RigidbodyMover _mover;
        
        protected override StateMachine InitStateMachine(EnemyStaticData staticData)
        {
            var data = (EyeStaticData) staticData;
            _hitByTrigger.Init(data.HitData, DefaultTargetTypes);
            _mover.SetSpeed(data.MoveSpeed);
            
            var chaseState = new ChaseState(_view, _mover, Target);
            var dashState = new SuperState(new MoveDirectionState(_view, _mover, Target), new BoostState(_mover, data.DashBoost));

            var chaseDashTransition = new Transition(dashState, new WaitTimeCondition(data.ChaseTime));
            var dashChaseTransition = new Transition(chaseState, new WaitTimeCondition(data.DashTime));

            chaseState.AddTransition(chaseDashTransition);
            dashState.AddTransition(dashChaseTransition);
            
            return new StateMachine(chaseState);
        }
    }
}
