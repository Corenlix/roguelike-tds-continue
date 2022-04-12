using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Items
{
    public class ItemPicker : MonoBehaviour
    {
        [SerializeField] private ItemPickTip _tip;
        private Player _pickPlayer;
        private readonly List<Item> _itemsToPick = new List<Item>();

        public void Init(Player player) => _pickPlayer = player;
        
        public void Pick()
        {
            if (_itemsToPick.Count == 0) return;
            Item itemToPick = _itemsToPick[0];
            itemToPick.Pick();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Item item))
            {
                if (item.NeedPressPickButton)
                {
                    if (item.NeedPressPickButton && _itemsToPick.Count == 0)
                        _tip.ShowTip(item);
                    _itemsToPick.Add(item);
                }
                else item.Pick();
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
