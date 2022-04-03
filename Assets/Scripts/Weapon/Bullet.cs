using UnityEngine;

namespace Weapon
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem bulletExplosion;
        [SerializeField] private float _speed;
        
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
            
            Instantiate(bulletExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
