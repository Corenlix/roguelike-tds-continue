using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Weapons.StaticData;
using Infrastructure;
using Infrastructure.Factory;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Entities.Weapons
{
    public class PlayerWeapons
    {
        public event Action<Weapon> Shot;
        
        private Weapon SelectedWeapon => _activeWeapons[_selectedWeaponIndex % _activeWeapons.Count];
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
        
        public void TryAddWeapon(WeaponId id)
        {
            var duplicate = _activeWeapons.FirstOrDefault(x => x.StaticData.WeaponId == id);
            if(duplicate != null)
                DropWeapon(duplicate);
            else if(_activeWeapons.Count >= _maxWeaponsCount)
                DropWeapon(SelectedWeapon);
            
            InstantiateWeapon(id);
            SwitchWeapon();
        }

        private Weapon InstantiateWeapon(WeaponId id)
        {
            var weaponInstance = _gameFactory.CreateWeapon(id, WeaponsContainer, WeaponsContainer.transform.position);
            var weaponScale = weaponInstance.transform.localScale;
            weaponScale.x = Mathf.Abs(weaponScale.x);
            weaponInstance.transform.localScale = weaponScale;
            _activeWeapons.Add(weaponInstance);
            return weaponInstance;
        }

        private void DropWeapon(Weapon weapon)
        {
            _gameFactory.CreateItem(weapon.StaticData.WeaponId, WeaponsContainer.position);
            Object.Destroy(weapon.gameObject);
            _activeWeapons.Remove(weapon);
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
