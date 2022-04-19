using System;
using Entities.Weapons;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace Items
{
    public class AmmoItem : Item, IAttractable
    {
        public MonoBehaviour MonoBehaviour => this;
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
