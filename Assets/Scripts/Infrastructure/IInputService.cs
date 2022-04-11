using UnityEngine;

namespace Infrastructure
{
    public interface IInputService : IService
    {
        Vector2 MoveAxis { get; }
        Vector2 LookPoint { get; }
        bool ShootButton { get; }
        bool PickButtonDown { get; }
        bool SwitchWeaponButtonDown { get; }
    }
}