using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Weapons.StaticData;
using Infrastructure;
using Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace Entities.Weapons
{
    public class PlayerWeapons
    {
        public event Action<Weapon> Shot;
        
        private Weapon SelectedWeapon => _activeWeapons[_selectedWeaponIndex];
        public Transform WeaponsContainer;
        private int _maxWeaponsCount = 3;
        private readonly List<Weapon> _activeWeapons = new List<Weapon>();
        private int _selectedWeaponIndex;
        private readonly IGameFactory _gameFactory;
        private readonly PlayerAmmoBelt _ammoBelt;

        public PlayerWeapons(IGameFactory gameFactory, PlayerAmmoBelt ammoBelt)
        {
            _ammoBelt = ammoBelt;
            _gameFactory = gameFactory;
        }

        public void TryShoot()
        {
            if (SelectedWeapon.IsReadyToShoot && _ammoBelt.TryTakeAmmo(SelectedWeapon.StaticData.AmmoType, SelectedWeapon.StaticData.AmmoPerShoot))
            {
                SelectedWeapon.Shoot();
                Shot?.Invoke(SelectedWeapon);
            }
        }

        public void AimTo(Vector3 position) => SelectedWeapon.AimTo(position);
        
        public bool TryAddWeapon(WeaponId id)
        {
            if (_activeWeapons.Count >= _maxWeaponsCount || _activeWeapons.Exists(x => x.StaticData.WeaponId == id))
                return false;

            var weaponInstance = _gameFactory.CreateWeapon(id, WeaponsContainer, WeaponsContainer.transform.position);
            var weaponScale = weaponInstance.transform.localScale;
            weaponScale.x = Mathf.Abs(weaponScale.x);
            weaponInstance.transform.localScale = weaponScale;
            _activeWeapons.Add(weaponInstance);

            if (_activeWeapons.Count > 1)
                weaponInstance.Disable();
            else SelectWeapon(0);

            return true;
        }

        public void RemoveSelectedWeapon()
        {
            SelectedWeapon.transform.parent = null;
            _activeWeapons.Remove(SelectedWeapon);
            SelectWeapon(_selectedWeaponIndex);
        }

        public void SwitchWeapon()
        {
            SelectWeapon(_selectedWeaponIndex + 1);
        }

        private void SelectWeapon(int index)
        {
            SelectedWeapon.Disable();
            _selectedWeaponIndex = index % _activeWeapons.Count;
            SelectedWeapon.Enable();
        }
    }
}
