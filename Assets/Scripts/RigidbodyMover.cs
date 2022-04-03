using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyMover : MonoBehaviour
{
    [SerializeField] private EntityView _view;
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;
    
    public void MoveTo(Vector2 destination)
    {
        _moveDirection = (destination - _rigidbody.position);
    }

    public void MoveByDirection(Vector2 direction)
    {
        _moveDirection = direction;
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 velocity = _moveDirection * _speed;
        _rigidbody.velocity = velocity;
        _view.SetRunState(velocity != Vector2.zero);
    }
}
