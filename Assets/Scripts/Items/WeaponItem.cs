using Entities.Weapons;
using Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace Items
{
    public class WeaponItem : Item
    {
        public override bool NeedPressInteractButton => true;
        public WeaponId WeaponId => _weaponId;
        protected override string OnPickText => InteractText;

        [SerializeField] private WeaponId _weaponId;
        private PlayerWeapons _playerWeapons;

        [Inject]
        private void Construct(PlayerWeapons playerWeapons)
        {
            _playerWeapons = playerWeapons;
        }
        
        protected override void OnInteract()
        {
            _playerWeapons.TryAddWeapon(_weaponId);
        }
    }
}
