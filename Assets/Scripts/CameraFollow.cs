using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
        [SerializeField] private CinemachineTargetGroup _targetGroup;

        public void Follow(Transform target)
        {
                _targetGroup.m_Targets[0].target = target;
        }
}