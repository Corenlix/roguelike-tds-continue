using UnityEngine;
using System.Collections.Generic;

public class AttractorAmmo : MonoBehaviour
{
    [SerializeField] private float _speedAttract;
    private readonly List<IAttractable> _attractables = new List<IAttractable>();
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IAttractable attractable))
        {
            _attractables.Add(attractable);
        }
    }

    private void Update()
    {
      MoveItem();
    }

    private void MoveItem()
    {
        for(int i = _attractables.Count - 1; i >= 0; i--)
        {
            if (!_attractables[i].MonoBehaviour)
                _attractables.Remove(_attractables[i]);
            
            else
                AttractTo(_attractables[i].MonoBehaviour);
        }
    }
    
    private void AttractTo(MonoBehaviour monoBehaviour)     
    {
        monoBehaviour.transform.position = Vector3.Lerp(monoBehaviour.transform.position, transform.position, _speedAttract * Time.deltaTime);
    }
}
