using System.Collections.Generic;
using Entities.HitBoxes;
using UnityEngine;

namespace Entities.Enemies.EnemyWeapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyBullet : MonoBehaviour
    {
        protected virtual List<HitBoxType> TargetTypes => new List<HitBoxType> { HitBoxType.Player, HitBoxType.Wall };
        [SerializeField] private GameObject _sparklesPrefab;
        private float _speed;
        private HitData _hitData;
        private Rigidbody2D _rigidbody2D;

        public void Init(HitData hitData, float speed)
        {
            _hitData = hitData;
            _speed = speed;
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
            if(other.TryGetComponent(out HitBox hitBox) && hitBox.TryHit(_hitData, transform, TargetTypes, _sparklesPrefab))
                Destroy(gameObject);
        }
    }
}