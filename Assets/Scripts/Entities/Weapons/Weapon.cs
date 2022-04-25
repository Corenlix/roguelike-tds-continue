using System;
using Entities.Weapons.StaticData;
using Infrastructure;
using Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace Entities.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public bool IsReadyToShoot => _timeRemainToShoot <= 0;

        public abstract WeaponStaticData StaticData { get; }

        [SerializeField] protected Transform _shootPoint;
        private float _timeRemainToShoot;
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
            transform.rotation = transform.LookAt2DWithFlip(targetPosition);
        }

        public void Shoot()
        {
            if (!IsReadyToShoot)
                throw new Exception();

            OnShoot();
            _timeRemainToShoot = StaticData.Delay;
        }

        protected abstract void OnShoot();

        protected Bullet CreateBullet(float addAngle = 0)
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