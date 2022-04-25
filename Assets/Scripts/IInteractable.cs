using System;
using UnityEngine;

public interface IInteractable
{
    event Action<IInteractable> Destroyed;
    Vector3 Position { get; }
    bool NeedPressInteractButton { get; }
    string InteractText { get; }
    void Interact();
}