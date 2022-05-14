using Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace Entities
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private IInput _input;

        [Inject]
        private void Construct(IInput input)
        {
            _input = input;
        }
    
        private void Update()
        {
            _player.Mover.MoveByDirection(_input.MoveAxis);
            _player.View.LookTo(_input.LookPoint);
            _player.Weapons.AimTo(_input.LookPoint);
            if (_input.ShootButton)
                _player.Weapons.TryShoot();
            if(_input.SwitchWeaponButtonDown)
                _player.Weapons.SwitchWeapon();
            if(_input.InteractButtonDown)
                _player.Interacter.Interact();
        }
    }
}