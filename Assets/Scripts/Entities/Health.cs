using System;
using UnityEngine;

namespace Entities
{
    public class Health : MonoBehaviour
    {
        public event Action HealthChanged;
        public event Action TookDamage;
        public event Action Died;

        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _health;
        
        private int _maxHealth;
        private int _health;

        public void Init(int maxHealth)
        {
            _maxHealth = _health = maxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            _health -= damage;
        
            TookDamage?.Invoke();
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
}