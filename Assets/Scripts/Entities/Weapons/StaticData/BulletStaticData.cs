using Entities.HitBoxes;
using Entities.Weapons;
using UnityEngine;

namespace Infrastructure
{
    public abstract class BulletStaticData : ScriptableObject
    {
        public abstract Bullet Prefab { get; }
        public BulletId Id;
        public float Speed;
        public HitData HitData;
    }
}