using Infrastructure;
using UnityEngine;
using Zenject;

namespace Entities.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public AmmoType AmmoType => StaticData.AmmoType;
        public int AmmoPerShoot => StaticData.AmmoPerShoot;
        public float ShakeIntensity => StaticData.ShakeIntensity;
        
        protected abstract WeaponStaticData StaticData { get; }
        
        [SerializeField] protected Transform _shootPoint;
        private float _timeRemainToShoot;
        private Vector3 _targetPosition;
        private IGameFactory _gameFactory;

        [Inject]
        private void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
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

        protected Bullet InstantiateBullet(float addAngle = 0)
        {
            return _gameFactory.CreateBullet(StaticData.BulletId, _shootPoint.position, transform.RotationWithFlip(addAngle));
        }

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