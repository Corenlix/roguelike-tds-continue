using UnityEngine;

namespace Infrastructure.Input
{
    public class StandaloneInput : IInput
    {
        private Camera Camera => _camera ? _camera : _camera = Camera.main;
        private Camera _camera;
    
        public Vector2 MoveAxis => new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
        public Vector2 LookPoint => Camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
        public bool ShootButton => UnityEngine.Input.GetMouseButton(0);
        public bool InteractButtonDown => UnityEngine.Input.GetKeyDown(KeyCode.E);
        public bool SwitchWeaponButtonDown => UnityEngine.Input.GetKeyDown(KeyCode.Q);
    }
}