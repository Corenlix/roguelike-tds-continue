using Entities.Enemies.EnemyStateMachine;
using Entities.Enemies.EnemyStateMachine.Conditions;
using Entities.Enemies.EnemyStateMachine.States;
using Entities.Enemies.StaticData;
using UnityEngine;

namespace Entities.Enemies
{
    public sealed class Spider : Enemy
    {
        [SerializeField] private EnemyView _view;
        [SerializeField] private HitByTrigger _hitByTrigger;
        [SerializeField] private Mover _mover;
        [SerializeField] private Mover _attackMover;
        private MeleeEnemyStaticData _data;
        
        protected override StateMachine InitStateMachine(EnemyStaticData staticData)
        {
            _data = (MeleeEnemyStaticData)staticData;
            _hitByTrigger.Init(_data.HitData, DefaultTargetTypes);
            _mover.SetSpeed(staticData.MoveSpeed);

            var randomWalkState = new RandomWalkState(_view, _mover, _data.RandomWalkDistance, _data.RandomWalkPeriod);
            var chaseState = new ChaseState(_view, _mover, Target);
            var prepareToAttackState = new PrepareAttackState(_view);
            var attackChaseState = new MoveDirectionState(_view, _attackMover, Target);
            var attackState = new SuperState( attackChaseState, new BoostState(_attackMover, _data.AttackBoost));

            randomWalkState.AddTransition(new Transition(chaseState, new InsideDistanceCondition(transform, Target, _data.WalkToChaseDistance)));
            
            chaseState.AddTransition(new Transition(randomWalkState, new OutsideDistanceCondition(transform, Target, _data.ChaseToWalkDistance)));
            chaseState.AddTransition(new Transition(prepareToAttackState, new InsideDistanceCondition(transform, Target, _data.ChaseToAttackDistance)));

            prepareToAttackState.AddTransition(new Transition(attackState, new WaitAnimationEndCondition(_view, AnimationNames.PrepareAttack)));
            
            attackState.AddTransition(new Transition(chaseState, new WaitAnimationEndCondition(_view, AnimationNames.Attack)));

            return new StateMachine(randomWalkState);
        }
    }
}
