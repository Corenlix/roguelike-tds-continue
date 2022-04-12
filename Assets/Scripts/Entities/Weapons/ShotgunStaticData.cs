using UnityEngine;

namespace Entities.Weapons
{
    [CreateAssetMenu(fileName = "Shotgun", menuName = "StaticData/Weapons/Shotgun")]
    public class ShotgunStaticData : WeaponStaticData
    {
        public int BulletsCount = 5;
        public float MaxDeviationAngle = 10;
    }
}