using UnityEngine;

namespace Infrastructure.Input
{
    public interface IInput : IService
    {
        Vector2 MoveAxis { get; }
        Vector2 LookPoint { get; }
        bool ShootButton { get; }
        bool InteractButtonDown { get; }
        bool SwitchWeaponButtonDown { get; }
    }
}