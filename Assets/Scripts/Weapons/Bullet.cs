using HitBoxes;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private HitData _hitData;
        [SerializeField] private float _speed;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    
        private void Start()
        {
            _rigidbody2D.velocity = transform.right * _speed;
            _hitData.Bullet = transform;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out HitBox hitBox) && hitBox.TryHit(_hitData))
                Destroy(gameObject);
        }
    }
}
