using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Weapons
{
    public class AmmoBelt : MonoBehaviour
    {
        public event Action AmmoCountChanged;
    
        private readonly Dictionary<AmmoType, int> _ammoCounts = new Dictionary<AmmoType, int>();

        public void AddAmmo(AmmoType ammoType, int count)
        {
            _ammoCounts[ammoType] += count;
            AmmoCountChanged?.Invoke();
        }
    
        public int GetAmmoCount(AmmoType ammoType) => _ammoCounts[ammoType];

        public void SubtractAmmo(AmmoType ammoType, int count = 1)
        {
            if (_ammoCounts[ammoType] < count)
                throw new InvalidOperationException();
        
            _ammoCounts[ammoType] -= count;
            AmmoCountChanged?.Invoke();
        }
    
        private void Awake()
        {
            Init();
            AddAmmo(AmmoType.Shotgun, 50);
            AddAmmo(AmmoType.Pistol, 50);
        }
    
        private void Init()
        {
            foreach (AmmoType ammoType in Enum.GetValues(typeof(AmmoType)))
            {
                _ammoCounts.Add(ammoType, 0);
            }
            AmmoCountChanged?.Invoke();
        }
    }
}
