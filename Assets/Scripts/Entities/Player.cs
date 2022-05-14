using System;
using Entities.Weapons;
using Infrastructure;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public class Player : MonoBehaviour
    {
        public RigidbodyMover RigidbodyMover => _mover;
        public Health Health => _health;
        public Transform WeaponsContainer => _weaponsContainer;
        public RigidbodyMover Mover => _mover;
        public EntityView View => _playerView;
        public Interacter Interacter => _interacter;
        public PlayerWeapons Weapons => _weapons;
        public PlayerAmmoBelt AmmoBelt => _playerAmmoBelt;

        [SerializeField] private Transform _weaponsContainer;
        [SerializeField] private RigidbodyMover _mover;
        [SerializeField] private EntityView _playerView;
        [SerializeField] private Health _health;
        [SerializeField] private Interacter _interacter;
        private PlayerWeapons _weapons;
        private PlayerAmmoBelt _playerAmmoBelt;

        public void Init(PlayerWeapons playerWeapons, PlayerAmmoBelt playerAmmoBelt)
        {
            _weapons = playerWeapons;
            _weapons.WeaponsContainer = _weaponsContainer;
            _playerAmmoBelt = playerAmmoBelt;
        }
        
        private void Start()
        {
            _mover.SetSpeed(8);
            _health.Init(100);
        }
    }
}