using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private static readonly int IsBroke = Animator.StringToHash("IsBroke");
    
    public void BrokePillar()
    {
        _animator.SetBool(IsBroke, true);
    }
}
