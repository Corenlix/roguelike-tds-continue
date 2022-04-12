using System.Collections.Generic;
using Entities.HitBoxes;
using Entities.Weapons.StaticData;
using Infrastructure;
using UnityEngine;

namespace Entities.Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Bullet : MonoBehaviour
    {
        protected abstract BulletStaticData BulletStaticData { get; }
        protected virtual List<HitBoxType> TargetTypes => new List<HitBoxType> { HitBoxType.Enemy, HitBoxType.Wall };
        [SerializeField] private GameObject _sparklesPrefab;
        private Rigidbody2D _rigidbody2D;
        
        public abstract void Init(BulletStaticData bulletData);

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _rigidbody2D.velocity = transform.right * BulletStaticData.Speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out HitBox hitBox) && hitBox.TryHit(BulletStaticData.HitData, transform, TargetTypes, _sparklesPrefab))
                Destroy(gameObject);
        }
    }
}
