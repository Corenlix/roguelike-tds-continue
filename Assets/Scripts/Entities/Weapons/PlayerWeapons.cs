using System;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace Entities.Weapons
{
    public class PlayerWeapons
    {
        public Weapon SelectedWeapon => _activeWeapons[_selectedWeaponIndex];
        public Transform WeaponsContainer;
        private int _maxWeaponsCount = 3;
        private readonly List<Weapon> _activeWeapons = new List<Weapon>();
        private int _selectedWeaponIndex;
        private IGameFactory _gameFactory;

        public PlayerWeapons(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        public bool TryAddWeapon(WeaponId id)
        {
            if (_activeWeapons.Count >= _maxWeaponsCount)
                return false;

            var weaponInstance = _gameFactory.CreateWeapon(id, WeaponsContainer, WeaponsContainer.transform.position);
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
