using Entities.Enemies.EnemyStateMachine;
using Entities.Enemies.EnemyStateMachine.Conditions;
using Entities.Enemies.EnemyStateMachine.States;
using Entities.Enemies.EnemyWeapons;
using Entities.Enemies.StaticData;
using UnityEngine;

namespace Entities.Enemies
{
    public class RangeEnemy : Enemy
    {
        [SerializeField] private EnemyView _view;
        [SerializeField] private EnemyShootWeapon _enemyWeapon;
        [SerializeField] private Mover _mover;

        protected override StateMachine InitStateMachine(EnemyStaticData staticData)
        {
            var data = (RangeEnemyStaticData)staticData;
            _enemyWeapon.Init(data.HitData, data.BulletSpeed, data.ShootDelay);
            _mover.SetSpeed(staticData.MoveSpeed);
            
            var weaponAttackState = new ShootState(_view, _enemyWeapon, Target);
            var chaseState = new ChaseState(_view, _mover, Target);

            var chaseShootState = new SuperState(weaponAttackState, chaseState);
            var randomWalkState = new RandomWalkState(_view, _mover, data.RandomWalkDistance, data.RandomWalkPeriod);
            var idleState = new IdleState();
           
            randomWalkState.AddTransition(new Transition(chaseShootState, new InsideDistanceCondition(transform, Target, data.WalkToChaseDistance)));
            
            chaseShootState.AddTransition(new Transition(randomWalkState, new OutsideDistanceCondition(transform, Target, data.ChaseToWalkDistance)));
            chaseShootState.AddTransition(new Transition(idleState, new InsideDistanceCondition(transform, Target, data.ChaseToIdleDistance)));

            idleState.AddTransition(new Transition(chaseShootState, new OutsideDistanceCondition(transform, Target, data.IdleToChaseDistance)));
            
            return new StateMachine(randomWalkState);
        }
    }
}