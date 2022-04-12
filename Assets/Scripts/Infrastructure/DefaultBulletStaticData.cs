using Entities.Weapons;
using UnityEngine;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "Static Data/Bullets/Bullet")]
    public class DefaultBulletStaticData : BulletStaticData
    {
        public override Bullet Prefab => _prefab;
        [SerializeField] private DefaultBullet _prefab;
    }
}