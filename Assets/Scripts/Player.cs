using Items;
using UnityEngine;
using Weapons;

public class Player : MonoBehaviour
{
    [SerializeField] private RigidbodyMover _mover;
    [SerializeField] private EntityView _playerView;
    [SerializeField] private AmmoBelt _ammoBelt;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Health _health;
    [SerializeField] private ItemPicker _itemPicker;

    private void Start()
    {
        _itemPicker.Init(this);
        _health.Died += OnDie;
    }

    public void Move(Vector3 direction) => _mover.MoveByDirection(direction);

    public void LookTo(Vector3 position)
    {
        _playerView.LookTo(position);
        _weapon.AimTo(position);
    }
    
    public void Shoot()
    {
        if (_ammoBelt.GetAmmoCount(_weapon.AmmoType) < _weapon.AmmoPerShoot) return;
        if (!_weapon.TryShoot()) return;
        
        _ammoBelt.SubtractAmmo(_weapon.AmmoType, _weapon.AmmoPerShoot);
        _playerView.Shoot(_weapon);
    }

    public void PickItem() => _itemPicker.Pick();

    public void AddAmmo(AmmoType ammoType, int count) => _ammoBelt.AddAmmo(ammoType, count);

    private void OnDie()
    {
        Destroy(gameObject);
        _health.Died -= OnDie;
    }
}
