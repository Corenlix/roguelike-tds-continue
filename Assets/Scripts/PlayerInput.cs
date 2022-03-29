using UnityEngine;

public class PlayerInput
{
    private RigidbodyMover _mover;
    private Weapon.Weapon _weapon;

    public PlayerInput(RigidbodyMover mover, Weapon.Weapon weapon)
    {
        _mover = mover;
        _weapon = weapon;
    }

    public void ReadInput()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _mover.MoveByDirection(moveDirection);
        if (Input.GetMouseButton(0))
        {
            _weapon.Shoot();
        }
    }
}
