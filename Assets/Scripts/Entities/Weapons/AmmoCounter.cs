using Infrastructure;
using TMPro;
using UnityEngine;
using Zenject;

namespace Entities.Weapons
{
    public class AmmoCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _ammoText;
        private AmmoType _ammoType;
        private PlayerAmmoBelt _playerAmmoBelt;

        [Inject]
        private void Construct(Player player)
        {
            _playerAmmoBelt = player.AmmoBelt;
        }

        public void ChangeAmmoType(AmmoType type)
        {
            _ammoType = type;
            UpdateCounter();
        }
        
        private void OnEnable()
        {
            _playerAmmoBelt.AmmoCountChanged += UpdateCounter;
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            _ammoText.text = $"[{_playerAmmoBelt.GetAmmoCount(_ammoType)}]";
        }

        private void OnDisable()
        {
            _playerAmmoBelt.AmmoCountChanged -= UpdateCounter;
        }
    }
}