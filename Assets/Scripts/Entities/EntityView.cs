using Entities.Weapons;
using UnityEngine;

namespace Entities
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
    
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");

        public void SetRunState(bool state)
        {
            _animator.SetBool(Run, state);
        }

        public void OnTakeDamage()
        {
            _animator.SetTrigger(TakeDamage);
        }

        public void LookTo(Vector3 position)
        {
            var aimDirection = position - transform.position;
            Vector3 rotationEuler = transform.rotation.eulerAngles;
            int xScaleModifier = aimDirection.x < 0 ? -1 : 1;
            transform.localScale = new Vector3(xScaleModifier * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.rotation = Quaternion.Euler(rotationEuler);   
        }
    }
}
