using UnityEngine;

public interface IInteractable
{
    Vector3 Position { get; }
    bool NeedPressInteractButton { get; }
    string InteractText { get; }
    void Interact();
}