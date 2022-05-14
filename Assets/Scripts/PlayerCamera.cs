using Entities;
using Entities.Weapons;
using UnityEngine;
using Zenject;

public class PlayerCamera : MonoBehaviour
{
    public CameraShaker Shaker => _shaker;
    public CameraFollow Follow => _follow;

    [SerializeField] private CameraShaker _shaker;
    [SerializeField] private CameraFollow _follow;
    private Player _player;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
        _follow.Follow(player.transform);
        player.Weapons.Shot += OnShoot;
    }

    private void OnShoot(Weapon weapon) =>_shaker.Shake(weapon.StaticData.ShakeIntensity, -weapon.transform.DirectionWithFlip(weapon.transform.right));

    private void OnDestroy()
    {
        _player.Weapons.Shot -= OnShoot;
    }
}
