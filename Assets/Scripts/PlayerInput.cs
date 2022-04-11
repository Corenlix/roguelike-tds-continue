using Infrastructure;
using UnityEngine;

public class StandaloneInputService : IInputService
{
    private Camera Camera => _camera ? _camera : _camera = Camera.main;
    private Camera _camera;
    
    public Vector2 MoveAxis => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    public Vector2 LookPoint => Camera.ScreenToWorldPoint(Input.mousePosition);
    public bool ShootButton => Input.GetMouseButton(0);
    public bool PickButtonDown => Input.GetKeyDown(KeyCode.E);
    public bool SwitchWeaponButtonDown => Input.GetKeyDown(KeyCode.Q);
}