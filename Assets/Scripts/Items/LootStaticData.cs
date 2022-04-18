using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Loot", menuName = "Static Data/Loot")]
    public class LootStaticData : ScriptableObject
    {
        public LootId LootId => _lootId;

        [SerializeField] private LootId _lootId;
        [SerializeField] private WeightsRandom<ItemId> _lootWeights;

        public ItemId Get() => _lootWeights.Get(ItemId.None);
    }
}