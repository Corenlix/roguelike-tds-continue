using System;
using Entities;
using UnityEngine;


public class WormHole : MonoBehaviour, IInteractable
{
    public event Action TransitionNextLevel;

    public event Action<IInteractable> Destroyed;
    public Vector3 Position => transform.position;
    public bool NeedPressInteractButton => true;
    public string InteractText => null;

    [Header("Attract parametres")] 
    [SerializeField] private float _forceAttract;
    [SerializeField] private float _criticCloseDistance;

    [Header("Time parametres")] 
    [SerializeField] [Range(1, 5)] private float _timeToTransitLvl;

    [Header("RotateAround parametres")]
    [SerializeField] [Range(0, 1)]private float speedRotate;
    private float _angle;
    private float _radius;
    
    private Transform _targetTransform;
    private RigidbodyMover _targetMover;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _targetTransform = player.GetComponent<Transform>();
            _targetMover = player.GetComponent<RigidbodyMover>();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        _targetTransform = null;
        _targetMover = null;
    }

    private void Update()
    {
        if (_targetTransform == null) return;
        AttractTarget(_targetTransform, _forceAttract, _criticCloseDistance);
        RotateAroundPoint(_targetTransform);
    }

    private void AttractTarget(Transform transformTarget, float forceAttract, float criticDistance)
    { 
        float distance = Vector3.Distance(_targetTransform.position, transform.position);
        if (distance <= criticDistance)
        {
            _forceAttract = 50f;
            _targetMover.SetSpeed(0);
            Invoke(nameof(Interact), _timeToTransitLvl);
        }
        transformTarget.position = Vector2.Lerp(transformTarget.position, transform.position,
            forceAttract * Time.deltaTime);
    }

    private void RotateAroundPoint(Transform targetTransform)
    {
        _radius = Vector3.Distance(targetTransform.position, transform.position);
        _radius -= Time.deltaTime / 2;
        _angle += 0.01f;

        if (_angle >= 360)
            _angle = 0;

        if (_radius <= 0)
            _radius = 0;

        float x = transform.position.x + Mathf.Cos(_angle * speedRotate) * _radius;
        float y = transform.position.y + Mathf.Sin(_angle * speedRotate) * _radius;
        targetTransform.position = new Vector3(x, y);
    }
    
    public void Interact()
    {
        TransitionNextLevel?.Invoke();
    }

    private void OnDestroy() 
    {
        Destroyed?.Invoke(this);
    }
}