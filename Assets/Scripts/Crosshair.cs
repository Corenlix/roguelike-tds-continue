using Infrastructure;
using UnityEngine;
using Zenject;

public class Crosshair : MonoBehaviour
{
    private IInput _input;

    [Inject]
    private void Construct(IInput input)
    {
        _input = input;
    }

    void Update()
    {
        transform.position = _input.LookPoint;
    }
}
