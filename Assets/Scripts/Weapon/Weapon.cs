using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Ammo.AmmoTypes _ammoTypes;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int maxCountAmmo;
    [SerializeField] private int currentCountAmmo;
    
    private void AddAmmo()
    {
        currentCountAmmo += 1;
    }
    private void FullCharge()
    {
        if (Input.GetKey(KeyCode.R))
        {
            currentCountAmmo = maxCountAmmo;
        }
    }
    private void Shoot(Ammo.AmmoTypes ammoType, ref int currentCountAmmo)
    {
        if (Input.GetMouseButton(0))
        {
            if (currentCountAmmo != 0)
            {
                var bullet =  Instantiate(bulletPrefab, firePoint.position, transform.rotation);
                bullet.Move(AimToTarget().normalized);
                currentCountAmmo -= 1;
            }
        }
    }
    private Vector2 AimToTarget()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        return target;
    }
    private void FixedUpdate()
    {
        AimToTarget();
        FullCharge();
        Shoot(_ammoTypes, ref currentCountAmmo);
    }
}

