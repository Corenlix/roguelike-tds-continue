using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float _timeModifier = 1 / 25f;
    [SerializeField] private float _offsetModifier = 1 / 50f;
    private float _remainShakeTime;
    private float _shakeTime;
    private float _intensity;
    private Vector2 _direction;

    private CinemachineVirtualCamera _camera;
    private CinemachineBasicMultiChannelPerlin _perlin;
    private CinemachineFramingTransposer _transposer;

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _transposer = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        _perlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float intensity, Vector2 direction)
    {
        _remainShakeTime = intensity * _timeModifier;
        _shakeTime = _remainShakeTime;
        _direction = direction.normalized;
        _intensity = intensity;
    }

    private void Update()
    {
        if (_shakeTime <= 0)
            return;

        _remainShakeTime -= Time.deltaTime;
        
        float currentIntensity = Mathf.Lerp(_intensity, 0f, 1 - (_remainShakeTime / _shakeTime));
        _transposer.m_ScreenX = -_direction.x * currentIntensity * _offsetModifier + 0.5f;
        _transposer.m_ScreenY = _direction.y * currentIntensity * _offsetModifier + 0.5f;
        _perlin.m_AmplitudeGain = currentIntensity;
    }
}
