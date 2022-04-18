using System.Collections.Generic;
using Entities.HitBoxes;
using UnityEngine;

namespace Entities.Enemies.EnemyWeapons
{
    public class Splash : EnemyWeapon
    {
        private List<HitBoxType> TargetTypes => new List<HitBoxType> { HitBoxType.Player };        
        [SerializeField] private GameObject _splashEffectPrefab;
        [SerializeField] private Collider2D _attackCollider;
        private HitData _hitData;

        public void Init(HitData hitData)
        {
            _hitData = hitData;
        }
        
        public override bool TryHit()
        {
            if(_splashEffectPrefab)
                Instantiate(_splashEffectPrefab, transform.position, Quaternion.identity);
            
            var overlapColliders = new List<Collider2D>();
            Physics2D.OverlapCollider(_attackCollider, new ContactFilter2D(), overlapColliders);
            foreach (var overlapCollider in overlapColliders)
            {
                if (overlapCollider.TryGetComponent(out HitBox hitBox))
                    hitBox.TryHit(_hitData, transform, TargetTypes);
            }
            
            return true;
        }
    }
}