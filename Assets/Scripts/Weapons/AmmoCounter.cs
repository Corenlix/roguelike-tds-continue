using System;
using TMPro;
using UnityEngine;

namespace Weapons
{
    public class AmmoCounter : MonoBehaviour
    {
        [SerializeField] private AmmoBelt _ammoBelt;
        [SerializeField] private AmmoType _ammoType;
        [SerializeField] private TextMeshProUGUI _ammoText;
        
        private void OnEnable()
        {
            _ammoBelt.AmmoCountChanged += UpdateCounter;
        }

        private void UpdateCounter()
        {
            _ammoText.text = $"[{_ammoBelt.GetAmmoCount(_ammoType)}]";
        }

        private void OnDisable()
        {
            _ammoBelt.AmmoCountChanged -= UpdateCounter;
        }
    }
}
