using System.Collections.Generic;
using Entities.HitBoxes;
using Items;
using UnityEngine;

namespace Entities.Enemies.StaticData
{
    public abstract class EnemyStaticData : ScriptableObject
    {
        public abstract Enemy Prefab { get; }

        public EnemyId Id;
        public LootId LootId;
        public float MoveSpeed;
        public HitData HitData;
    }
}