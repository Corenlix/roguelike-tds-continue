using UnityEngine;

namespace Entities.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public AmmoType AmmoType => _ammoType;
        public int AmmoPerShoot => _ammoPerShoot;
        public float ShakeIntensity => _shakeIntensity;
        
        [SerializeField] protected Transform shootPoint;

        [SerializeField] private AmmoType _ammoType;
        [SerializeField] private int _ammoPerShoot = 1;
        [SerializeField] private float _shakeIntensity;
        [SerializeField] private float _delay;
        private float _timeRemainToShoot;
        private Vector3 _targetPosition;

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
        
        public void AimTo(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            transform.rotation = transform.LookAt2DWithFlip(_targetPosition);
        }

        public bool TryShoot()
        {
            if (_timeRemainToShoot > 0)
                return false;

            OnShoot();
            _timeRemainToShoot = _delay;
            return true;
        }

        protected abstract void OnShoot();

        private void Awake()
        {
            Enable();
        }
        
        private void Update()
        {
            _timeRemainToShoot -= Time.deltaTime;
        }
    }
}