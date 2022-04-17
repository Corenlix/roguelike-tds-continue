using System.Collections.Generic;
using Entities.Enemies.StaticData;
using Entities.HitBoxes;
using UnityEngine;

namespace Entities.Enemies
{
    public class Spider : MeleeEnemy
    {
        private readonly List<HitBoxType> _hitBoxTypes = new List<HitBoxType>() {HitBoxType.Player};
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out HitBox hitBox))
            {
                hitBox.TryHit(Data.HitData, transform, _hitBoxTypes, null);
            }
        }
    }
}