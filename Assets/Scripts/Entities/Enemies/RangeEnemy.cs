using Entities.Enemies.EnemyStateMachine;
using Entities.Enemies.EnemyStateMachine.Conditions;
using Entities.Enemies.EnemyStateMachine.States;
using Entities.Enemies.EnemyWeapons;
using Entities.Enemies.StaticData;
using Entities.Weapons;
using Infrastructure;
using UnityEngine;

namespace Entities.Enemies
{
    public class RangeEnemy : Enemy
    {
        [SerializeField] private EnemyWeapon _enemyWeapon;
        private StateMachine _stateMachine;
        private RangeEnemyStaticData _data;
        
        protected override void OnInit(EnemyStaticData data)
        {
            _data = (RangeEnemyStaticData)data;
            _enemyWeapon.Init(_data.HitData, _data.BulletSpeed, _data.ShootDelay);
            InitStateMachine();
        }

        public void RangeAttack(Vector3 target)
        {
            _view.LookTo(target);
            _enemyWeapon.AimTo(target);
            _enemyWeapon.TryShoot();   
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