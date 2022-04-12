using UnityEngine;

namespace Entities.Weapons.StaticData
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "Static Data/Bullets/Bullet")]
    public class DefaultBulletStaticData : BulletStaticData
    {
        public override Bullet Prefab => _prefab;
        [SerializeField] private DefaultBullet _prefab;
    }
}