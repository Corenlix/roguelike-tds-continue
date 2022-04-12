using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Popup
{
    public class PopupSpawner : IService
    {
        private const string PopupViewsPath = "PopupViews";
        private Dictionary<PopupType, PopupView> _popupViews;

        public PopupSpawner()
        {
            Load();
        }

        private void Load()
        {
            _popupViews = Resources
                .LoadAll<PopupView>(PopupViewsPath)
                .ToDictionary(x => x.Type, x => x);
        }

        public void SpawnPopup(PopupType popupType, Vector2 position, string popupText)
        {
            PopupView popupView = Object.Instantiate(ViewForType(popupType), position, quaternion.identity);
            popupView.Init(popupText);
        }

        private PopupView ViewForType(PopupType type) => _popupViews[type];
    }
}
