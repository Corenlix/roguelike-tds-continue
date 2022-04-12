using UnityEngine;

namespace Entities.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public AmmoType AmmoType => StaticData.AmmoType;
        public int AmmoPerShoot => StaticData.AmmoPerShoot;
        public float ShakeIntensity => StaticData.ShakeIntensity;
        
        protected abstract WeaponStaticData StaticData { get; }
        
        [SerializeField] protected Transform shootPoint;
        private float _timeRemainToShoot;
        private Vector3 _targetPosition;

        public abstract void Init(WeaponStaticData weaponStaticData);

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
            _timeRemainToShoot = StaticData.Delay;
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

    public abstract class WeaponStaticData : ScriptableObject
    {
        public Weapon Prefab;
        public WeaponId WeaponId;
        public AmmoType AmmoType;
        public int AmmoPerShoot = 1;
        public float ShakeIntensity;
        public float Delay;
    }
}