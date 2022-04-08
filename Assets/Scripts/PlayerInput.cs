using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private  Player _player;
    [SerializeField] private Crosshair _crosshair;

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _player.Move(moveDirection);
        _player.LookTo(_crosshair.transform.position);
        
        if (Input.GetMouseButton(0))
            _player.Shoot();
        
        if(Input.GetKeyDown(KeyCode.E))
           _player.PickItem();
    }
}
