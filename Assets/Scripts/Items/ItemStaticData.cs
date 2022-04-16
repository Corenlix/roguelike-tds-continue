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
        None = -1,
        PistolAmmoMediumPack= 0,
        ShotgunAmmoMediumPack = 1,
        RifleAmmoMediumPack = 2,
        
        Pistol = 100,
        Shotgun = 101,
        MachineGun = 102,
        Riffle = 103,
        AssaultRiffle = 104,
    }
}