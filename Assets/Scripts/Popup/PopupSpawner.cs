using System;
using Unity.Mathematics;
using UnityEngine;

namespace Popup
{
    public class PopupSpawner : MonoBehaviour
    {
        public static  PopupSpawner Instance => _instance;
        private static PopupSpawner _instance;
        
        [SerializeField] private PopupView _damagePopup;
        [SerializeField] private PopupView _itemPickPopup;

        private void Awake()
        {
            if(_instance)
                Destroy(_instance);
            _instance = this;
        }
   
        public void SpawnPopup(PopupType popupType, Vector2 position, string popupText)
        {
            PopupView popupView = Instantiate(GetPopupFromType(popupType), position, quaternion.identity);
            popupView.Init(popupText);
        }

        private PopupView GetPopupFromType(PopupType type)
        {
            return type switch
            {
                PopupType.Damage => _damagePopup,
                PopupType.ItemPick => _itemPickPopup,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }

    public enum PopupType
    {
        Damage,
        ItemPick,
    }
}
