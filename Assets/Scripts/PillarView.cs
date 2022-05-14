using UnityEngine;

public class PillarView : MonoBehaviour
{
    private Pillar _pillar;
    private Material _material;

    private static readonly int Fade = Shader.PropertyToID("_Fade");
    private float _fade = 1f;
    private bool _isDissolving;
    
    private void Awake()
    {
        _material = GetComponentInChildren<SpriteRenderer>().material;
        _pillar = GetComponent<Pillar>();
    }

    private void OnEnable()
    {
        _pillar.Disappear += OnDisappear;
    }

    private void OnDisable()
    {
        _pillar.Disappear -= OnDisappear;
    }

    private void OnDisappear(bool isDissolving)
    {
        _isDissolving = isDissolving;
        if (_isDissolving)
        {
            _material.SetFloat(Fade, _fade);
            _fade -= Time.deltaTime;
            if (_fade <= 0)
            {
                _fade = 0f;
                _isDissolving = false;
                Destroy(gameObject);
            }
        }
    }
        
    private void Update()
    {
        OnDisappear(_isDissolving);
    }
}
