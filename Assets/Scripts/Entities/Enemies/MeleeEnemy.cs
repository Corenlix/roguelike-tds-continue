using Entities.Enemies.EnemyStateMachine;
using Entities.Enemies.EnemyStateMachine.Conditions;
using Entities.Enemies.EnemyStateMachine.States;
using Entities.Enemies.EnemyWeapons;
using Entities.Enemies.StaticData;
using UnityEngine;

namespace Entities.Enemies
{
    public class MeleeEnemy : Enemy
    {
        [SerializeField] private Mover _mover;
        [SerializeField] private Mover _attackMover;
        [SerializeField] private Splash _weapon;
        
        protected override StateMachine InitStateMachine(EnemyStaticData staticData)
        {
            var data = (MeleeEnemyStaticData)staticData;
            _weapon.Init(staticData.HitData);
            _mover.SetSpeed(staticData.MoveSpeed);

            var randomWalkState = new RandomWalkState(_view, _mover, data.RandomWalkDistance, data.RandomWalkPeriod);
            var chaseState = new ChaseState(_view, _mover, Target);
            var prepareToAttackState = new PrepareAttackState(_view);
            var attackChaseState = new MoveDirectionState(_view, _attackMover, Target);
            var attackState = new SuperState( attackChaseState, new MeleeAttackState(_view, _weapon), new BoostState(_attackMover, data.AttackBoost));

            randomWalkState.AddTransition(new Transition(chaseState, new InsideDistanceCondition(transform, Target, data.WalkToChaseDistance)));
            
            chaseState.AddTransition(new Transition(randomWalkState, new OutsideDistanceCondition(transform, Target, data.ChaseToWalkDistance)));
            chaseState.AddTransition(new Transition(prepareToAttackState, new InsideDistanceCondition(transform, Target, data.ChaseToAttackDistance)));

            prepareToAttackState.AddTransition(new Transition(attackState, new WaitAnimationEndCondition(_view, AnimationNames.PrepareAttack)));
            
            attackState.AddTransition(new Transition(chaseState, new WaitAnimationEndCondition(_view, AnimationNames.Attack)));

            return new StateMachine(randomWalkState);
        }
    }
}
