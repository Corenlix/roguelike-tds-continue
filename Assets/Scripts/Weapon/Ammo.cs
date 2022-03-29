using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public event Action OnTakeAmmo;

    private bool isActive;
    
    public enum AmmoTypes
    {
        ShotGunAmmo,
        LaserAmmo,
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (TryGetComponent(out Player player))
        {
            OnTakeAmmo?.Invoke();
        }
    }
}
