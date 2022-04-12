using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RigidbodyMover : MonoBehaviour
    {
        [SerializeField] private EntityView _view;
        [SerializeField] private float _speed;
        private  Rigidbody2D _rigidbody;
        private Vector2 _moveDirection;

        private KnockBackForces _knockBackForces = new KnockBackForces();

        public void AddForce(Force force) => _knockBackForces.AddForce(force);

        public void Init(float speed)
        {
            _speed = speed;
        }
        
        public void MoveTo(Vector2 destination)
        {
            MoveByDirection(destination - _rigidbody.position);
        }

        public void MoveByDirection(Vector2 direction)
        {
            _moveDirection = direction.normalized;
        }
    
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        protected virtual void FixedUpdate()
        {
            _knockBackForces.UpdateForces();
            Vector2 velocity = _moveDirection * _speed ;
            _rigidbody.velocity = velocity + _knockBackForces.GetTotalValue();
            _view.SetRunState(velocity != Vector2.zero);
            Debug.DrawLine(transform.position, transform.position + (Vector3)velocity.normalized, Color.yellow, 0.1f);
        }
    
    }
}
