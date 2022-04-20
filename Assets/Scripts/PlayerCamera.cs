using Entities;
using Entities.Weapons;
using Infrastructure;
using UnityEngine;
using Zenject;

public class PlayerCamera : MonoBehaviour
{
    public CameraShaker Shaker => _shaker;
    public CameraFollow Follow => _follow;

    [SerializeField] private CameraShaker _shaker;
    [SerializeField] private CameraFollow _follow;

    [Inject]
    private void Construct(Player player, PlayerWeapons playerWeapons)
    {
        _follow.Follow(player.transform);
        playerWeapons.Shot += weapon => _shaker.Shake(weapon.ShakeIntensity, -weapon.transform.DirectionWithFlip(weapon.transform.right));
    }
}
