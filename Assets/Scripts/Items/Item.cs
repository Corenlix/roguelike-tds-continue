using System;
using Infrastructure.Popup;
using UnityEngine;
using Zenject;

namespace Items
{
    public abstract class Item : MonoBehaviour, IInteractable
    {
        public event Action<IInteractable> Destroyed;
        public string Name;
        public Vector3 Position => transform.position;
        public abstract bool NeedPressInteractButton { get; }
        public string InteractText => Name; 
        protected abstract string OnPickText { get; }
        
        private PopupSpawner _popupSpawner;

        [Inject]
        private void Construct(PopupSpawner popupSpawner)
        {
            _popupSpawner = popupSpawner;
        }
        
        public void Interact()
        {
            _popupSpawner.SpawnPopup(PopupType.ItemPick, transform.position, InteractText);
            OnInteract();
            Destroy(gameObject);
        }
        
        protected abstract void OnInteract();
        
        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
