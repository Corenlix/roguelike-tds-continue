using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Weapons;

public class CameraShaker : MonoBehaviour
{
    private float _amplitudeGainShake = 1f;
    private float _frequencyGainShake = 0f;
    private float _timeShake;
    
    private CinemachineVirtualCamera _camera;
    private CinemachineBasicMultiChannelPerlin _channelPerlin;
    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _channelPerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    private void SetupTimeShake(float time)
    {
        _timeShake = time;
    }
    private void OnEnable()
    {
        Bullet.OnShakeCamera += Shake;
    }

    private void OnDisable()
    {
        Bullet.OnShakeCamera -= Shake;
    }

    private void Shake()
    {
        _channelPerlin.m_AmplitudeGain = _amplitudeGainShake;
        _channelPerlin.m_FrequencyGain = _frequencyGainShake;
        SetupTimeShake(0.1f);
    }
    
    private void Update()
    {
        if (_timeShake > 0)
        {
            _timeShake -= Time.deltaTime;
            if (_timeShake <= 0)
            {
                _channelPerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                _channelPerlin.m_AmplitudeGain = 0f;
                _channelPerlin.m_FrequencyGain = 0f;
            }
        }
    }
}
