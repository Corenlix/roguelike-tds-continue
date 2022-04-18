using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Items
{
    [CreateAssetMenu(fileName = "Loot", menuName = "Static Data/Loot")]
    public class LootStaticData : ScriptableObject
    {
        public LootId LootId => _lootId;
        
        [SerializeField] private List<LootWeight> _lootWeights;
        [SerializeField] private LootId _lootId;
        
        public ItemId Get()
        {
            float totalWeight = _lootWeights.Sum(x => x.Weight);
            float random = Random.Range(0, totalWeight);
            float curRandomSum = 0;
            foreach (var lootChance in _lootWeights)
            {
                curRandomSum += lootChance.Weight;
                if (random <= curRandomSum)
                    return lootChance.Item;
            }

            return ItemId.None;
        }
    }
}