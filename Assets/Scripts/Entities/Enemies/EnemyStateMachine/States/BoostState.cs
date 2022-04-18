namespace Entities.Enemies.EnemyStateMachine.States
{
    public class BoostState : State
    {
        private readonly float _modifier;
        private readonly Mover _mover;

        public BoostState(Mover mover, float modifier)
        {
            _mover = mover;
            _modifier = modifier;
        }
        
        public override void Enter()
        {
            _mover.SetSpeed(_mover.GetSpeed() * _modifier);
        }

        public override void Tick()
        {
            
        }

        public override void Exit()
        {
            _mover.SetSpeed(_mover.GetSpeed() / _modifier);
        }
    }
}