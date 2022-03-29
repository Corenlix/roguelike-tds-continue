using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        transform.position = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
