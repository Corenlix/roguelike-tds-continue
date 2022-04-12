using UnityEngine;

namespace Entities.Weapons.StaticData
{
    public abstract class WeaponStaticData : ScriptableObject
    {
        public abstract Weapon Prefab { get; }
        public WeaponId WeaponId;
        public BulletId BulletId;
        public AmmoType AmmoType;
        public int AmmoPerShoot = 1;
        public float ShakeIntensity;
        public float Delay;
    }
}