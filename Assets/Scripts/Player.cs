using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RigidbodyMover _mover;

    private PlayerInput _input;

    private void Start()
    {
        _input = new PlayerInput(_mover);
    }

    private void Update()
    {
        _input.ReadInput();
    }
}
