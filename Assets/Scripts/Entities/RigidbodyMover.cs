using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RigidbodyMover : Mover
    {
        [SerializeField] private EntityView _view;
        [SerializeField] private float _speed;
        private  Rigidbody2D _rigidbody;
        private Vector2 _moveDirection;

        private KnockBackForces _knockBackForces = new KnockBackForces();

        public void AddForce(Force force) => _knockBackForces.AddForce(force);

        public override void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public override float GetSpeed() => _speed;
        
        public override void MoveTo(Vector2 position)
        {
            MoveByDirection(position - _rigidbody.position);
        }

        public void MoveByDirection(Vector2 direction)
        {
            _moveDirection = direction.normalized;
        }

        public override void Stop()
        {
            _moveDirection = Vector2.zero;
        }
    
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _knockBackForces.UpdateForces();
            Vector2 velocity = _moveDirection * _speed ;
            _rigidbody.velocity = velocity + _knockBackForces.GetTotalValue();
            _view.SetRunState(velocity != Vector2.zero);
        }
    }
}
