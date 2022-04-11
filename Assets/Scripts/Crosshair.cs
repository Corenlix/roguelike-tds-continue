using Infrastructure;
using UnityEngine;
using Zenject;

public class Crosshair : MonoBehaviour
{
    private IInputService _inputService;

    [Inject]
    private void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }

    void Update()
    {
        transform.position = _inputService.LookPoint;
    }
}
