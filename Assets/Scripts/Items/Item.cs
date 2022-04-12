using Infrastructure.Popup;
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
        
        public void Pick()
        {
            _popupSpawner.SpawnPopup(PopupType.ItemPick, transform.position, PickText);
            OnPick();
            Destroy(gameObject);
        }
        
        protected abstract void OnPick();
    }
}
