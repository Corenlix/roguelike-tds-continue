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
        private PlayerAmmoBelt _playerAmmoBelt;

        [Inject]
        private void Construct(PlayerAmmoBelt playerAmmoBelt)
        {
            _playerAmmoBelt = playerAmmoBelt;
        }
        
        private void OnEnable()
        {
            _playerAmmoBelt.AmmoCountChanged += UpdateCounter;
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
