using System;
using Popup;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _bulletExplosion;
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        public static event Action OnShakeCamera;

        private Rigidbody2D _rigidbody2D;
        private LayerMask _interactiveLayers;
        
        public void Init(LayerMask layerMask)
        {
            _interactiveLayers = layerMask;
        }
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    
        private void Start()
        {
            _rigidbody2D.velocity = transform.right * _speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
           
            if ((_interactiveLayers.value & (1 << other.gameObject.layer)) == 0)
                return;
            
            OnShakeCamera?.Invoke();
            if(other.TryGetComponent(out Health health))
            {
               health.DealDamage(_damage);
               PopupSpawner.Instance.SpawnPopup(other.transform.position, _damage);
            }
            
            Instantiate(_bulletExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
