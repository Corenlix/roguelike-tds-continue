using Entities.Weapons;
using UnityEngine;
using Zenject;

namespace Items
{
    public class WeaponItem : Item
    {
        public override bool NeedPressPickButton => true;
        protected override string PickText => Name;

        [SerializeField] private WeaponId _weaponId;
        private PlayerWeapons _playerWeapons;

        [Inject]
        private void Construct(PlayerWeapons playerWeapons)
        {
            _playerWeapons = playerWeapons;
        }
        
        protected override void OnPick()
        {
            _playerWeapons.TryAddWeapon(_weaponId);
        }
    }
}
