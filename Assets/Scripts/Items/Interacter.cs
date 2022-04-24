using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Items
{
    public class Interacter : MonoBehaviour
    {
        [SerializeField] private InteractTip _tip;
        private readonly List<IInteractable> _activeInteractables = new List<IInteractable>();

        public void Interact()
        {
            if (_activeInteractables.Count == 0) return;
            IInteractable itemToPick = _activeInteractables[0];
            itemToPick.Interact();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                if (interactable.NeedPressInteractButton)
                {
                    if (interactable.NeedPressInteractButton && _activeInteractables.Count == 0)
                        _tip.ShowTip(interactable);
                    _activeInteractables.Add(interactable);
                }
                else interactable.Interact();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                _tip.HideTip();
                _activeInteractables.Remove(interactable);

                if(_activeInteractables.Count > 0)
                    _tip.ShowTip(_activeInteractables[0]);
            }
        }
    }
}
