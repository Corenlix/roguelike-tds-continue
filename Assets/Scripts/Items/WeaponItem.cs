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
        private IGameFactory _gameFactory;

        [Inject]
        private void Construct(PlayerWeapons playerWeapons, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _playerWeapons = playerWeapons;
        }
        
        protected override void OnInteract()
        {
            if (!_playerWeapons.TryAddWeapon(_weaponId))
                _gameFactory.CreateItem(_weaponId, transform.position);
        }
    }
}
