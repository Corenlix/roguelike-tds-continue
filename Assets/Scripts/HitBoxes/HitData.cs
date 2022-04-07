using System;
using System.Collections.Generic;
using UnityEngine;

namespace HitBoxes
{
    [Serializable]
    public class HitData
    { 
        public List<HitBoxType> TargetTypes;
        public GameObject SparklesPrefab;
        public int Damage;
        [HideInInspector] public Transform Bullet;
    }
}