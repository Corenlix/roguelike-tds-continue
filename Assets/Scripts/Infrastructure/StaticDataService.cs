using System.Collections.Generic;
using System.Linq;
using Entities.Weapons;
using UnityEngine;

namespace Infrastructure
{
    public class StaticDataService : IStaticDataService
    {
        private const string EnemiesDataPath = "Static Data/Enemies";
        private const string WeaponsDataPath = "Static Data/Weapons";
        private const string BulletsDataPath = "Static Data/Bullets";
        private Dictionary<EnemyId, EnemyStaticData> _enemies;
        private Dictionary<WeaponId, WeaponStaticData> _weapons;
        private Dictionary<BulletId, BulletStaticData> _bullets;

        public StaticDataService()
        {
            Load();
        }

        public void Load()
        {
            _enemies = Resources
                .LoadAll<EnemyStaticData>(EnemiesDataPath)
                .ToDictionary(x => x.Id, x => x);
            
            _weapons = Resources
                .LoadAll<WeaponStaticData>(WeaponsDataPath)
                .ToDictionary(x => x.WeaponId, x => x);  
            
            _bullets = Resources
                .LoadAll<BulletStaticData>(BulletsDataPath)
                .ToDictionary(x => x.Id, x => x);
        }

        public EnemyStaticData ForEnemy(EnemyId id) => _enemies[id];
        
        public WeaponStaticData ForWeapon(WeaponId id) => _weapons[id];

        public BulletStaticData ForBullet(BulletId id) => _bullets[id];
    }
}