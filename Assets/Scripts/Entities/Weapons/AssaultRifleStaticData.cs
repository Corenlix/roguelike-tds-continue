using Entities.Weapons.StaticData;
using UnityEngine;

namespace Entities.Weapons
{
    [CreateAssetMenu(fileName = "Item", menuName = "Static Data/Weapons/Assault Riffle")]
    public class AssaultRifleStaticData : WeaponStaticData
    {
        public override Weapon Prefab => _prefab;
        public int ShootsInBurst;
        [SerializeField] private Weapon _prefab;
    }
}