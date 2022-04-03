using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DamagePopup : MonoBehaviour
{
   private Animation _animation;
   
   private void Awake()
   {
      _animation = GetComponent<Animation>();
   }

   private void Start()
   {
      _animation.Play();
      
   }
   
   private void Destroy()
   {
      Destroy(gameObject);
   }
}
