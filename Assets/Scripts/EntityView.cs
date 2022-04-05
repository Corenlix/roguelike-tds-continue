using UnityEngine;
using Weapons;

public class EntityView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private static readonly int Run = Animator.StringToHash("Run");

    public void SetRunState(bool state)
    {
        _animator.SetBool(Run, state);
    }

    public void LookTo(Vector3 position)
    {
        var aimDirection = position - transform.position;
        Vector3 rotationEuler = transform.rotation.eulerAngles;
        int xScaleModifier = aimDirection.x < 0 ? -1 : 1;
        transform.localScale = new Vector3(xScaleModifier * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        transform.rotation = Quaternion.Euler(rotationEuler);   
    }

    public virtual void Shoot(Weapon weapon)
    {
    }
}
