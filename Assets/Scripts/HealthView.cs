using Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _healthStripe;
    [SerializeField] private TextMeshProUGUI _healthText;
    
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