using Entities;
using Popup;
using UnityEngine;

namespace Items
{
    public abstract class Item : MonoBehaviour
    {
        public abstract bool NeedPressPickButton { get; }
        protected abstract string PickText { get; }
        public string Name;

        public void Pick(Player player)
        {
            PopupSpawner.Instance.SpawnPopup(PopupType.ItemPick, transform.position, PickText);
            OnPick(player);
            Destroy(gameObject);
        }
        
        protected abstract void OnPick(Player player);
    }
}
