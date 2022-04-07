using Unity.Mathematics;
using UnityEngine;

namespace HitBoxes
{
    public class WallHitBox : HitBox
    {
        public override HitBoxType HitBoxType => HitBoxType.Wall;
        
        protected override void Hit(HitData hitData)
        {
            Instantiate(hitData.SparklesPrefab, hitData.Bullet.position, quaternion.identity);
        }
    }
}