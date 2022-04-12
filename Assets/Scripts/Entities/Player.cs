using System;
using Entities.Weapons;
using Infrastructure;
using Infrastructure.Input;
using Items;
using UnityEngine;
using Zenject;

namespace Entities
{
    public class Player : MonoBehaviour
    {
        public event Action<Weapon> Shot;
        public Health Health => _health;

        [SerializeField] private Transform _weaponsContainer;
        [SerializeField] private RigidbodyMover _mover;
        [SerializeField] private EntityView _playerView;
        [SerializeField] private Health _health;
        [SerializeField] private ItemPicker _itemPicker;
        private IInput _input;
        private PlayerWeapons _weapons;

        [Inject]
        private void Construct(IInput input, PlayerWeapons playerWeapons)
        {
            _weapons = playerWeapons;
            _weapons.WeaponsContainer = _weaponsContainer;
            _input = input;
        }

        private void Start()
        {
            _itemPicker.Init(this);
            _weapons.TryAddWeapon(WeaponId.Pistol);
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

        private void Shoot()
        {
            var weapon = _weapons.SelectedWeapon;
            if (!weapon.TryShoot()) return;
            
            Shot?.Invoke(weapon);
        }
    }
}
