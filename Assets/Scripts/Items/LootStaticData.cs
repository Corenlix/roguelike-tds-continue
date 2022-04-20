using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Loot", menuName = "Static Data/Loot")]
    public class LootStaticData : ScriptableObject
    {
        public LootId LootId => _lootId;

        [SerializeField] private LootId _lootId;
        [SerializeField] private List<WeightsRandom<ItemId>> _lootWeights;

        public List<ItemId> Get()
        {
            var result = new List<ItemId>();
            _lootWeights.ForEach(loot=>result.Add(loot.Get(ItemId.None)));
            return result;
        }
    }
}