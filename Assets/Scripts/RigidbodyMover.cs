using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyMover : MonoBehaviour
{
    [SerializeField] private EntityView _view;
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;
    private List<Force> _activeForces = new List<Force>();
    
    private Vector2 CalculateForces()
    {
        Vector2 result = Vector2.zero;
        foreach (var force in _activeForces)
        {
            result += force.Intensity * force.Direction;
        }
        return result;
    }

    private void UpdateForces()
    {
        _activeForces.ForEach(x => x.Tick());
        _activeForces = _activeForces.Where(x => x.Intensity > 0).ToList();
    }
    public void AddForce(Force force)
    {
        _activeForces.Add(force);
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
        UpdateForces();
        Vector2 velocity = _moveDirection * _speed ;
        _rigidbody.velocity = velocity + CalculateForces();
        _view.SetRunState(velocity != Vector2.zero);
        Debug.DrawLine(transform.position, transform.position + (Vector3)velocity.normalized, Color.yellow, 0.1f);
    }
    
}
