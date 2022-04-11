using Entities;
using Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image _healthStripe;
    [SerializeField] private TextMeshProUGUI _healthText;
    private Health _health;

    [Inject]
    private void Construct(IPlayerFactory playerFactory)
    {
        _health = playerFactory.Player.Health;
    }
    
    private void OnEnable()
    {
        _health.HealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar()
    {
        _healthStripe.fillAmount = (float)_health.CurrentHealth / _health.MaxHealth;
        _healthText.text = $"{_health.CurrentHealth}/{_health.MaxHealth}";
    }
}