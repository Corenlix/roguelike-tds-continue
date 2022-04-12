using Items;
using UnityEngine;

[CreateAssetMenu(menuName = "Static Data/Chest", fileName = "Chest")]
public class ChestStaticData : ScriptableObject
{
    public ChestId ChestId;
    public Chest Prefab;
    public LootId LootId;
}