using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RigidbodyMover _mover;
    [SerializeField] private EntityView _playerView;
    [SerializeField] private Crosshair _crosshair;
    [SerializeField] private Weapons.Weapon _weapon;
    
    private PlayerInput _input;

    private void Start()
    {
        _input = new PlayerInput(_mover, _weapon);
    }

    private void Update()
    {
        _input.ReadInput();
        _playerView.LookAt(_crosshair.transform.position);
        _weapon.AimTo(_crosshair.transform.position);
    }
}
