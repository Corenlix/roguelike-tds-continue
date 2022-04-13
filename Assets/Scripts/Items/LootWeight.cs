using System;
using UnityEngine.Serialization;

namespace Items
{
    [Serializable]
    public class LootWeight
    {
        public ItemId Item;
        public float Weight;
    }
}