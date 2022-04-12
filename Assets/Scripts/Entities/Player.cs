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
        private IInput _input;

        [Inject]
        private void Construct(IInput input)
        {
            _input = input;
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
            Move(_input.MoveAxis);
            LookTo(_input.LookPoint);
            if(_input.ShootButton)
                Shoot();
            if(_input.SwitchWeaponButtonDown)
                SwitchWeapon();
            if(_input.PickButtonDown)
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
