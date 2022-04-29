using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Entities.Weapons
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private Image _weaponImage;
        [SerializeField] private AmmoCounter _ammoCounter;
        private PlayerWeapons _playerWeapons;

        [Inject]
        private void Construct(Player player)
        {
            _playerWeapons = player.Weapons;
        }

        private void OnEnable()
        {
            _playerWeapons.WeaponSwitched += UpdateWeapon;
            UpdateWeapon(_playerWeapons.SelectedWeapon);
        }

        private void UpdateWeapon(Weapon weapon)
        {
            _weaponImage.sprite = weapon.StaticData.WeaponIcon;
            _ammoCounter.ChangeAmmoType(weapon.StaticData.AmmoType);
        }

        private void OnDisable()
        {
            _playerWeapons.WeaponSwitched -= UpdateWeapon;
        }
    }
}