using Entities.Weapons;
using Infrastructure;
using Pathfinding;
using UnityEngine;
using Zenject;

namespace Entities.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private PathfindMover _pathfindMover;
        [SerializeField] private EntityView _view;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Health _health;
    
        protected Transform Target;

        [Inject]
        private void Construct(Pathfinder pathfinder, Player player)
        {
            _health.Died += OnDie;
            _pathfindMover.Init(pathfinder);
            SetTarget(player.transform);
        }

        public abstract void Init(EnemyStaticData staticData);

        public void SetTarget(Transform target)
        {
            Target = target;
        }

        public void Attack(Vector3 target)
        {
            _view.LookTo(target);
            _weapon.AimTo(target);
            _weapon.TryShoot();
        }

        public void MoveTo(Vector3 destination) => _pathfindMover.SetMovePoint(destination);

        public void Stop() => _pathfindMover.Reset();

        private void OnDie()
        {
            _health.Died -= OnDie;
            Destroy(gameObject);
        }
    }
}
