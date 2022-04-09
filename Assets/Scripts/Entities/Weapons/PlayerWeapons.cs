using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Weapons
{
    public class PlayerWeapons : MonoBehaviour
    {
        public Weapon SelectedWeapon => _activeWeapons[_selectedWeaponIndex];
        [SerializeField] private int _maxWeaponsCount;
        [SerializeField] private Transform _weaponsContainer;
        private readonly List<Weapon> _activeWeapons = new List<Weapon>();
        private int _selectedWeaponIndex;
        
        public bool TryAddWeapon(Weapon weapon)
        {
            if (_activeWeapons.Count >= _maxWeaponsCount)
                return false;
            
            _activeWeapons.Add(weapon);
            weapon.transform.parent = _weaponsContainer;
            weapon.transform.localPosition = Vector3.zero;

            if (_activeWeapons.Count > 1)
                weapon.Disable();
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
