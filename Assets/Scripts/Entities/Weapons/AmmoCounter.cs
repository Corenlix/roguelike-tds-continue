using Infrastructure;
using TMPro;
using UnityEngine;
using Zenject;

namespace Entities.Weapons
{
    public class AmmoCounter : MonoBehaviour
    {
        [SerializeField] private AmmoType _ammoType;
        [SerializeField] private TextMeshProUGUI _ammoText;
        private AmmoBelt _ammoBelt;

        [Inject]
        private void Construct(Player player)
        {
            _ammoBelt = player.AmmoBelt;
        }
        
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
