using UnityEngine;

namespace Entities.HitBoxes
{
    public abstract class HitBox : MonoBehaviour
    {
        public abstract HitBoxType HitBoxType { get; }

        public bool TryHit(HitData hitData)
        {
            if (!hitData.TargetTypes.Contains(HitBoxType))
                return false;

            Hit(hitData);
            return true;
        }
        
        protected abstract void Hit(HitData hitData);
    }

    public enum HitBoxType
    {
        Player,
        Enemy,
        Wall,
    }
}