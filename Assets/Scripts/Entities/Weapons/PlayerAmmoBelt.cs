using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Weapons
{
    public class PlayerAmmoBelt
    {
        public event Action AmmoCountChanged;
    
        private readonly Dictionary<AmmoType, int> _ammoCounts = new Dictionary<AmmoType, int>();

        public PlayerAmmoBelt()
        {
            InitAmmoCounts();
        }

        private void InitAmmoCounts()
        {
            foreach (AmmoType ammoType in Enum.GetValues(typeof(AmmoType)))
            {
                _ammoCounts.Add(ammoType, 0);
            }
        }

        public void AddAmmo(AmmoType ammoType, int count)
        {
            _ammoCounts[ammoType] += count;
            AmmoCountChanged?.Invoke();
        }
    
        public int GetAmmoCount(AmmoType ammoType) => _ammoCounts[ammoType];

        public bool TryTakeAmmo(AmmoType ammoType, int count = 1)
        {
            if (_ammoCounts[ammoType] < count)
                return false;
        
            _ammoCounts[ammoType] -= count;
            AmmoCountChanged?.Invoke();
            return true;
        }
    }
}
