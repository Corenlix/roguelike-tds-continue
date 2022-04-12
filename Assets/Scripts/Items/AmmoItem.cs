using Entities.Weapons;
using UnityEngine;
using Zenject;

namespace Items
{
    public class AmmoItem : Item
    {
        public override bool NeedPressInteractButton => false;
        protected override string OnPickText => $"+{_count}";
        
        [SerializeField] private AmmoType _ammoType;
        [SerializeField] private int _count;
        private PlayerAmmoBelt _ammoBelt;

        [Inject]
        private void Construct(PlayerAmmoBelt ammoBelt)
        {
            _ammoBelt = ammoBelt;
        }
        
        protected override void OnInteract()
        {
            _ammoBelt.AddAmmo(_ammoType, _count);
        }
    }
}
