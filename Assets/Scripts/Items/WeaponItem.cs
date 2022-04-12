using Entities;
using Entities.Weapons;
using UnityEngine;

namespace Items
{
    public class WeaponItem : Item
    {
        public override bool NeedPressPickButton => true;
        protected override string PickText => Name;

        [SerializeField] private WeaponId _weaponId;
    
        protected override void OnPick(Player player)
        {
            player.TryAddWeapon(_weaponId);
        }
    }
}
