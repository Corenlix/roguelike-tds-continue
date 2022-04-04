using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action HealthChanged;
    public event Action Damaged;
    public event Action Died;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _health;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _health;
     
    public void DealDamage(int damage)
    {
        _health -= damage;
        
        Damaged?.Invoke();
        HealthChanged?.Invoke();
        
        if(_health <= 0)
            OnDied();
    }
    
    private void ResetHealth()
    {
        _health = _maxHealth;
        HealthChanged?.Invoke();
    }
    
    private void OnDied()
    {
        Died?.Invoke();
    }

    private void Start()
    {
        ResetHealth();
    }
}