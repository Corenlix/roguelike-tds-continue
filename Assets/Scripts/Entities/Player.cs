using System;
using Entities.Weapons;
using Infrastructure;
using Items;
using UnityEngine;
using Zenject;

namespace Entities
{
    public class Player : MonoBehaviour
    {
        public event Action<Weapon> Shot;
        public Health Health => _health;
        public AmmoBelt AmmoBelt => _ammoBelt;
        
        [SerializeField] private RigidbodyMover _mover;
        [SerializeField] private EntityView _playerView;
        [SerializeField] private AmmoBelt _ammoBelt;
        [SerializeField] private PlayerWeapons _weapons;
        [SerializeField] private Health _health;
        [SerializeField] private ItemPicker _itemPicker;
        [SerializeField] private Weapon _startWeapon;
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Start()
        {
            _itemPicker.Init(this);
            _weapons.TryAddWeapon(Instantiate(_startWeapon));
            _health.Died += OnDie;
        }

        private void OnDie()
        {
            Destroy(gameObject);
            _health.Died -= OnDie;
        }

        private void Update()
        {
            Move(_inputService.MoveAxis);
            LookTo(_inputService.LookPoint);
            if(_inputService.ShootButton)
                Shoot();
            if(_inputService.SwitchWeaponButtonDown)
                SwitchWeapon();
            if(_inputService.PickButtonDown)
                PickItem();
        }

        private void Move(Vector3 direction) => _mover.MoveByDirection(direction);

        private void LookTo(Vector3 position)
        {
            _playerView.LookTo(position);
            _weapons.SelectedWeapon.AimTo(position);
        }

        private void SwitchWeapon() => _weapons.SwitchWeapon();

        private void PickItem() => _itemPicker.Pick();

        public bool TryAddWeapon(Weapon weapon) =>_weapons.TryAddWeapon(weapon);

        private void Shoot()
        {
            var weapon = _weapons.SelectedWeapon;
            if (_ammoBelt.GetAmmoCount(weapon.AmmoType) < weapon.AmmoPerShoot) return;
            if (!weapon.TryShoot()) return;
        
            _ammoBelt.SubtractAmmo(weapon.AmmoType, weapon.AmmoPerShoot);
            _playerView.Shoot(weapon);
            Shot?.Invoke(weapon);
        }

        public void AddAmmo(AmmoType ammoType, int count) => _ammoBelt.AddAmmo(ammoType, count);
    }
}
