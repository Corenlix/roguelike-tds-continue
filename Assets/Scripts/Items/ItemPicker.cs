using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Items
{
    public class ItemPicker : MonoBehaviour
    {
        [SerializeField] private ItemPickTip _tip;
        private Player _pickPlayer;
        private readonly List<IInteractable> _itemsToPick = new List<IInteractable>();

        public void Init(Player player) => _pickPlayer = player;
        
        public void Pick()
        {
            if (_itemsToPick.Count == 0) return;
            IInteractable itemToPick = _itemsToPick[0];
            itemToPick.Interact();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                if (interactable.NeedPressInteractButton)
                {
                    if (interactable.NeedPressInteractButton && _itemsToPick.Count == 0)
                        _tip.ShowTip(interactable);
                    _itemsToPick.Add(interactable);
                }
                else interactable.Interact();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Item item))
            {
                _tip.HideTip();
                _itemsToPick.Remove(item);
            }

            if(_itemsToPick.Count > 0)
                _tip.ShowTip(_itemsToPick[0]);
        }
    }
}
