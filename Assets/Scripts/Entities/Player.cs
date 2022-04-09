using Entities.Weapons;
using Items;
using UnityEngine;

namespace Entities
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private RigidbodyMover _mover;
        [SerializeField] private EntityView _playerView;
        [SerializeField] private AmmoBelt _ammoBelt;
        [SerializeField] private PlayerWeapons _weapons;
        [SerializeField] private Health _health;
        [SerializeField] private ItemPicker _itemPicker;
        
        [SerializeField] private Weapon _startWeapon;

        private void Start()
        {
            _itemPicker.Init(this);
            _weapons.TryAddWeapon(Instantiate(_startWeapon));
            _health.Died += OnDie;
        }

        public void Move(Vector3 direction) => _mover.MoveByDirection(direction);

        public void LookTo(Vector3 position)
        {
            _playerView.LookTo(position);
            _weapons.SelectedWeapon.AimTo(position);
        }

        public bool TryAddWeapon(Weapon weapon) =>_weapons.TryAddWeapon(weapon);

        public void SwitchWeapon() => _weapons.SwitchWeapon();
        
        public void Shoot()
        {
            var weapon = _weapons.SelectedWeapon;
            if (_ammoBelt.GetAmmoCount(weapon.AmmoType) < weapon.AmmoPerShoot) return;
            if (!weapon.TryShoot()) return;
        
            _ammoBelt.SubtractAmmo(weapon.AmmoType, weapon.AmmoPerShoot);
            _playerView.Shoot(weapon);
        }

        public void PickItem() => _itemPicker.Pick();

        public void AddAmmo(AmmoType ammoType, int count) => _ammoBelt.AddAmmo(ammoType, count);

        private void OnDie()
        {
            Destroy(gameObject);
            _health.Died -= OnDie;
        }
    }
}
