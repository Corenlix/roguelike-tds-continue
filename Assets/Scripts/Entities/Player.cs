using System;
using Entities.Weapons;
using Infrastructure;
using Infrastructure.Input;
using Items;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Entities
{
    public class Player : MonoBehaviour
    {
        public RigidbodyMover RigidbodyMover => _mover;
        public Health Health => _health;

        [SerializeField] private Transform _weaponsContainer;
        [SerializeField] private RigidbodyMover _mover;
        [SerializeField] private EntityView _playerView;
        [SerializeField] private Health _health;
        [SerializeField] private Interacter _interacter;
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
            _mover.SetSpeed(8);
            _health.Init(100);
        }
        
        private void Update()
        {
            _mover.MoveByDirection(_input.MoveAxis);
            LookTo(_input.LookPoint);
            if (_input.ShootButton)
                _weapons.TryShoot();
            if(_input.SwitchWeaponButtonDown)
                _weapons.SwitchWeapon();
            if(_input.InteractButtonDown)
                _interacter.Interact();
        }
        
        private void LookTo(Vector3 position)
        {
            _playerView.LookTo(position);
            _weapons.AimTo(position);
        }
    }
}
