using Unity.Mathematics;
using UnityEngine;

namespace Entities.HitBoxes
{
    public class WallHitBox : HitBox
    {
        public override HitBoxType HitBoxType => HitBoxType.Wall;
        
        protected override void Hit(HitData hitData, Transform bullet, GameObject sparkles)
        {
            Instantiate(sparkles, bullet.position, quaternion.identity);
        }
    }
}