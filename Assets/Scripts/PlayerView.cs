using System;
using UnityEngine;
using Weapons;

public class PlayerView : EntityView
{
        [SerializeField] private CameraShaker _cameraShaker;
        
        public override void Shoot(Weapon weapon)
        {
                _cameraShaker.Shake(weapon.ShakeIntensity, -weapon.transform.DirectionWithFlip(weapon.transform.right));
                base.Shoot(weapon);
        }
}