using UnityEngine;

namespace Entities.Weapons.StaticData
{
    [CreateAssetMenu(fileName = "Pistol", menuName = "Static Data/Weapons/Pistol")]
    public class PistolStaticData : WeaponStaticData
    {
        public override Weapon Prefab => _prefab;
        [SerializeField] private Pistol _prefab;
    }
}