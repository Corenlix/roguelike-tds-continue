using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Static Data/Item")]
    public class ItemStaticData : ScriptableObject
    {
        public ItemId Id;
        public Item Prefab;
    }

    public enum ItemId
    {
        PistolAmmoMediumPack,
        ShotgunAmmoMediumPack,
        Pistol,
        Shotgun,
    }
}