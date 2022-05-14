using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WormHoleView : MonoBehaviour
{
    [SerializeField] private int _speedRotateSprite;
    private void Update()
    {
        transform.Rotate(0, 0, _speedRotateSprite * Time.deltaTime);
    }
}
