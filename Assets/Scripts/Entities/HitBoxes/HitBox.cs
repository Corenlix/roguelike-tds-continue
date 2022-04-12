using System.Collections.Generic;
using UnityEngine;

namespace Entities.HitBoxes
{
    public abstract class HitBox : MonoBehaviour
    {
        public abstract HitBoxType HitBoxType { get; }

        public bool TryHit(HitData hitData, Transform bullet, List<HitBoxType> targetTypes, GameObject sparkles)
        {
            if (!targetTypes.Contains(HitBoxType))
                return false;

            Hit(hitData, bullet, sparkles);
            return true;
        }
        
        protected abstract void Hit(HitData hitData, Transform bullet, GameObject sparkles);
    }
}