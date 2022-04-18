using System;
using UnityEngine;

namespace Entities
{
    public abstract class Mover : MonoBehaviour
    {
        public abstract void SetSpeed(float speed);
        public abstract float GetSpeed();
        public abstract void MoveTo(Vector2 position);
        public abstract void Stop();
    }
}