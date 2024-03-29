using System;
using System.Collections;
using Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace Entities
{
    public abstract  class EntityView : MonoBehaviour
    {
        public event Action<AnimatorStateInfo> StateEntered;
        public event Action<AnimatorStateInfo> StateExited;
        
        [SerializeField] protected Animator _animator;
        [SerializeField] private Health _health;
        [SerializeField] private GameObject _corpse;
    
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");

        public void SetRunState(bool state)
        {
            _animator.SetBool(Run, state);
        }

        public virtual void LookTo(Vector3 position)
        {
            var aimDirection = position - transform.position;
            Vector3 rotationEuler = transform.rotation.eulerAngles;
            int xScaleModifier = aimDirection.x < 0 ? -1 : 1;
            transform.localScale = new Vector3(xScaleModifier * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.rotation = Quaternion.Euler(rotationEuler);   
        }

        private void OnEnable()
        {
            var stateReporter = _animator.GetBehaviour<AnimatorStateReporter>();
            if (!stateReporter) throw new Exception();
            stateReporter.StateEntered += OnStateEnter;
            stateReporter.StateExited += OnStateExit;
            _health.TookDamage += OnTakeDamage;
            _health.Died += OnDie;
        }

        private void OnStateEnter(AnimatorStateInfo state) => StateEntered?.Invoke(state);

        private void OnStateExit(AnimatorStateInfo state) => StateExited?.Invoke(state);

        private void OnTakeDamage()
        {
            _animator.SetTrigger(TakeDamage);
        }

        private void OnDie()
        {
            var corpse = Instantiate(_corpse, transform.position, Quaternion.identity);
            corpse.transform.localScale = transform.lossyScale;
            Destroy(gameObject);
            Destroy(corpse);
        }
        
        private void OnDisable()
        {
            var stateReporter = _animator.GetBehaviour<AnimatorStateReporter>();
            if (!stateReporter) return;
            stateReporter.StateEntered -= OnStateEnter;
            stateReporter.StateExited -= OnStateExit;
            _health.TookDamage -= OnTakeDamage;
            _health.Died -= OnDie;
        }
    }
    
}
