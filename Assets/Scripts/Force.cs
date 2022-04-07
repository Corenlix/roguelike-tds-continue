using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force
{
  private const float DecaySpeed = 50f; 
  public float Intensity => _intensity;
  public Vector2 Direction => _direction;
  
  private float _intensity; 
  private readonly Vector2 _direction;

  public Force(float intensity, Vector2 direction)
  {
    _intensity = intensity;
    _direction = direction.normalized;
  }

  public void Tick()
  {
    _intensity -= DecaySpeed * Time.fixedDeltaTime;
  }
}
