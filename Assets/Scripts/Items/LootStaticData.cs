using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items
{
    [CreateAssetMenu(fileName = "Loot", menuName = "Static Data/Loot")]
    public class LootStaticData : ScriptableObject
    {
        public LootId LootId => _lootId;
        
        [SerializeField] private List<LootChance> _lootChances;
        [SerializeField] private LootId _lootId;
        
        public ItemId Get()
        {
            float random = Random.Range(0, 100);
            float curRandom = 0;
            foreach (var lootChance in _lootChances)
            {
                curRandom += lootChance.Chance;
                if (curRandom > 100)
                    throw new ArgumentOutOfRangeException();

                if (curRandom <= random)
                    return lootChance.Item;
            }

            return ItemId.None;
        }
    }
}