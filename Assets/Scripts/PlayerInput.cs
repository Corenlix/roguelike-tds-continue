using UnityEngine;

public class PlayerInput
{
    private RigidbodyMover _mover;

    public PlayerInput(RigidbodyMover mover)
    {
        _mover = mover;
    }

    public void ReadInput()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _mover.MoveByDirection(moveDirection);
    }
}
