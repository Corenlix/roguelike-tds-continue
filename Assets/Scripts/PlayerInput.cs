using UnityEngine;

public class PlayerInput
{
    private readonly Player _player;
    private readonly Crosshair _crosshair;

    public PlayerInput(Player player, Crosshair crosshair)
    {
        _player = player;
        _crosshair = crosshair;
    }

    public void ReadInput()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _player.Move(moveDirection);
        
        _player.LookTo(_crosshair.transform.position);
        
        if (Input.GetMouseButton(0))
            _player.Shoot();
    }
}
