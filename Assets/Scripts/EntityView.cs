using UnityEngine;

public class EntityView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private static readonly int Run = Animator.StringToHash("Run");

    public void SetRunState(bool state)
    {
        _animator.SetBool(Run, state);
    }

    public void LookAt(Vector2 point)
    {
        var aimDirection = point - (Vector2)transform.position;
        Vector3 rotationEuler = transform.rotation.eulerAngles;
        int xScaleModifier = aimDirection.x < 0 ? -1 : 1;
        transform.localScale = new Vector3(xScaleModifier * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        transform.rotation = Quaternion.Euler(rotationEuler);   
    }
}
