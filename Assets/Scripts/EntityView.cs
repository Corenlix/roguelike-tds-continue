using UnityEngine;

public class EntityView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private static readonly int Run = Animator.StringToHash("Run");

    public void SetRunState(bool state)
    {
        _animator.SetBool(Run, state);
    }
}
