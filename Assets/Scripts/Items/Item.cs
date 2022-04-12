using Entities;
using Popup;
using UnityEngine;
using Zenject;

namespace Items
{
    public abstract class Item : MonoBehaviour
    {
        public abstract bool NeedPressPickButton { get; }
        protected abstract string PickText { get; }
        public string Name;
        private PopupSpawner _popupSpawner;

        [Inject]
        private void Construct(PopupSpawner popupSpawner)
        {
            _popupSpawner = popupSpawner;
        }
        
        public void Pick(Player player)
        {
            _popupSpawner.SpawnPopup(PopupType.ItemPick, transform.position, PickText);
            OnPick(player);
            Destroy(gameObject);
        }
        
        protected abstract void OnPick(Player player);
    }
}
