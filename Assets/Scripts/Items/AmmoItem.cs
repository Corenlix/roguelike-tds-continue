using Entities.Weapons;
using UnityEngine;
using Zenject;

namespace Items
{
    public class AmmoItem : Item
    {
        public override bool NeedPressPickButton => false;
        protected override string PickText => $"+{_count}";
        
        [SerializeField] private AmmoType _ammoType;
        [SerializeField] private int _count;
        private PlayerAmmoBelt _ammoBelt;

        [Inject]
        private void Construct(PlayerAmmoBelt ammoBelt)
        {
            _ammoBelt = ammoBelt;
        }
        
        protected override void OnPick()
        {
            _ammoBelt.AddAmmo(_ammoType, _count);
        }
    }
}
