using Entities;
using Entities.Weapons;
using UnityEngine;

namespace Items
{
    public class WeaponItem : Item
    {
        public override bool NeedPressPickButton => true;
        protected override string PickText => Name;

        [SerializeField] private Weapon _weaponPrefab;
    
        protected override void OnPick(Player player)
        {
            var weapon = Instantiate(_weaponPrefab);
            player.TryAddWeapon(weapon);
        }
    }
}
