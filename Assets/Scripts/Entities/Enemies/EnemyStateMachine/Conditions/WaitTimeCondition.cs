using System;

namespace Entities.Enemies.EnemyStateMachine.Conditions
{
    public class WaitTimeCondition : Condition
    {
        private readonly float _timeSec;
        private DateTime _startTime;
        private bool _needStartTimer = true;
        
        public WaitTimeCondition(float timeSec)
        {
            _timeSec = timeSec;
            _startTime = DateTime.UtcNow;
        }
        
        public override bool IsConditionMet()
        {
            if (_needStartTimer)
            {
                _startTime = DateTime.UtcNow;
                _needStartTimer = false;
            }

            return DateTime.UtcNow.Subtract(_startTime).TotalSeconds >= _timeSec;
        }

        public override void Reset()
        {
            _needStartTimer = true;
        }
    }
}