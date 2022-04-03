using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PopupSpawner : MonoBehaviour
{
    public static  PopupSpawner Instance => _instance;
    
    [SerializeField] private GameObject _prefabPopup;
    private static PopupSpawner _instance;

    private void Awake()
   {
       _instance = this;
   }
   
   public void SpawnPopup(Vector2 position)
   {
       Instantiate(_prefabPopup, position, quaternion.identity);
   }
}
