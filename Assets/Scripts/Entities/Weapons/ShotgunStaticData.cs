using UnityEngine;

namespace Entities.Weapons
{
    [CreateAssetMenu(fileName = "Shotgun", menuName = "Static Data/Weapons/Shotgun")]
    public class ShotgunStaticData : WeaponStaticData
    {
        public override Weapon Prefab => _prefab;
        public int BulletsCount = 5;
        public float MaxDeviationAngle = 10;
        [SerializeField] private Shotgun _prefab;
    }
}