using System;
using UnityEngine.Serialization;

namespace Items
{
    [Serializable]
    public class LootChance
    {
        public ItemId Item;
        [FormerlySerializedAs("Chance")] public float Weight;
    }
}