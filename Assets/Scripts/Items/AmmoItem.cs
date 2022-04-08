using UnityEngine;
using Weapons;

namespace Items
{
    public class AmmoItem : Item
    {
        public override bool NeedPressPickButton => false;
        protected override string PickText => $"+{_count}";
        
        [SerializeField] private AmmoType _ammoType;
        [SerializeField] private int _count;
    
        protected override void OnPick(Player player)
        {
            player.AddAmmo(_ammoType, _count);
        }
    }
}
