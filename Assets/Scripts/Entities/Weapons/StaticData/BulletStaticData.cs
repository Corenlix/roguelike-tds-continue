using Entities.HitBoxes;
using UnityEngine;

namespace Entities.Weapons.StaticData
{
    public abstract class BulletStaticData : ScriptableObject
    {
        public abstract Bullet Prefab { get; }
        public BulletId Id;
        public float Speed;
        public HitData HitData;
    }
}