namespace Entities.Enemies.EnemyStateMachine.Conditions
{
    public abstract class Condition
    {
        public abstract bool IsConditionMet();
        public abstract void Reset();
    }
}
