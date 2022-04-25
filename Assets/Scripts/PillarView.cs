using UnityEngine;

public class PillarView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private static readonly int IsBroken = Animator.StringToHash("IsBroken");
    
    public void BrokePillar()
    {
        _animator.SetBool(IsBroken, true);
    }
}
