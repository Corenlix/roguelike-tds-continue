using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Progress;
using Infrastructure.SaveLoad;
using UnityEngine;

namespace Entities.Weapons
{
    public class PlayerAmmoBelt : IProgressClient
    {
        public event Action AmmoCountChanged;
        private Dictionary<AmmoType, int> _ammoCounts = new Dictionary<AmmoType, int>();

        public void AddAmmo(AmmoType ammoType, int count)
        {
            if(!_ammoCounts.ContainsKey(ammoType))
                _ammoCounts.Add(ammoType, 0);
            
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

        public void Save(ISaveLoadService saveLoadService)
        {
            saveLoadService.SetValue(_ammoCounts, SaveLoadKey.PlayerAmmo);
        }

        public void Load(ISaveLoadService saveLoadService)
        {
            var defaultAmmoCounts = ((AmmoType[]) Enum.GetValues(typeof(AmmoType))).ToDictionary(x => x, x => 20);
            _ammoCounts = saveLoadService.GetValue<Dictionary<AmmoType, int>>(SaveLoadKey.PlayerAmmo, defaultAmmoCounts);
        }
    }
}