using UnityEngine;
using Weapons;

public class Player : MonoBehaviour
{
    [SerializeField] private RigidbodyMover _mover;
    [SerializeField] private EntityView _playerView;
    [SerializeField] private Crosshair _crosshair;
    [SerializeField] private Weapon _weapon;
    
    private PlayerInput _input;

    private void Start()
    {
        _input = new PlayerInput(this, _crosshair);
    }

    private void Update()
    {
        _input.ReadInput();
    }

    public void Move(Vector3 direction) => _mover.MoveByDirection(direction);

    public void LookTo(Vector3 position)
    {
        _playerView.LookTo(position);
        _weapon.AimTo(position);
    }
    
    public void Shoot()
    {
        if(_weapon.TryShoot())
            _playerView.Shoot(_weapon);
    }
}
