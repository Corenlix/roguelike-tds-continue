using Entities.Enemies;
using Unity.Mathematics;
using UnityEngine;

namespace Entities
{
    public class EyeView : EnemyView
    {
        [SerializeField] private float rotateSpeed;
        
        public override void LookTo(Vector3 position)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, transform.position.LookAt2D(position), rotateSpeed * Time.deltaTime);
        }
    }
}