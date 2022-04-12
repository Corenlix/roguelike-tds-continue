using Entities.Weapons.StaticData;
using Infrastructure;

namespace Entities.Weapons
{
    public class DefaultBullet : Bullet
    {
        protected override BulletStaticData BulletStaticData => _bulletData;
        private BulletStaticData _bulletData;

        public override void Init(BulletStaticData bulletData)
        {
            _bulletData = bulletData;
        }
    }
}