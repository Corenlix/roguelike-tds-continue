using UnityEngine;

namespace Entities.Enemies.EnemyStateMachine.States
{
    public class RandomWalkState : State
    {
        private readonly int _walkDistance;
        private readonly float _walkPeriod;
        private readonly EnemyView _enemyView;
        private readonly Mover _mover;
        private float _timeToWalk;

        public RandomWalkState(EnemyView enemyView, Mover mover, int walkDistance, float walkPeriod)
        {
            _mover = mover;
            _enemyView = enemyView;
            _walkDistance = walkDistance;
            _walkPeriod = walkPeriod;
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
            Vector2 newPoint = _enemyView.transform.position; 
            newPoint += new Vector2(Random.Range(-_walkDistance, _walkDistance), Random.Range(-_walkDistance, _walkDistance));
            _mover.MoveTo(newPoint);
        }
        
        public override void Exit()
        {
            _mover.Stop();
        }
    }
}