using Entities;
using UnityEngine;

namespace Infrastructure
{
    public interface IGameFactory
    {
        Player CreatePlayer(Vector3 at);
        PlayerCamera CreatePlayerCamera();
        GameObject CreateHud();
    }
}