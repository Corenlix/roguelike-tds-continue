using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletForce;

    public event Action DealDamage;
    
    private Rigidbody2D _rigidbody2D;
    
    private Vector2 directionShoot;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        Move(directionShoot);
    }
    
    public void Move(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction * bulletForce,ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       Destroy(gameObject);
    }
}
