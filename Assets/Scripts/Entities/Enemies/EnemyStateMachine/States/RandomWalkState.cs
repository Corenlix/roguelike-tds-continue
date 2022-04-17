using UnityEngine;

namespace Entities.Enemies.EnemyStateMachine.States
{
    public class RandomWalkState : State
    {
        private readonly Enemy _enemy;
        private readonly int _walkDistance;
        private readonly float _walkPeriod;
        private float _timeToWalk;

        public RandomWalkState(Enemy enemy, int walkDistance, float walkPeriod)
        {
            _walkDistance = walkDistance;
            _walkPeriod = walkPeriod;
            _enemy = enemy;
        }
        
        public override void Enter()
        {
            MoveToNewPoint();
            _timeToWalk = _walkPeriod;
        }

        public override void Tick()
        {
            
            _timeToWalk -= Time.deltaTime;
            if (_timeToWalk > 0)
                return;

            MoveToNewPoint();
            _timeToWalk = _walkPeriod;
        }

        private void MoveToNewPoint()
        {
            Vector2 newPoint = _enemy.transform.position; 
            newPoint += new Vector2(Random.Range(-_walkDistance, _walkDistance), Random.Range(-_walkDistance, _walkDistance));
            _enemy.MoveTo(newPoint);
        }
        
        public override void Exit()
        {
            _enemy.Stop();
        }
    }
}