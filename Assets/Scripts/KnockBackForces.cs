using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KnockBackForces
{
    private List<Force> _activeForces = new List<Force>();
    
    public Vector2 GetTotalValue()
    {
        Vector2 result = Vector2.zero;
        foreach (var force in _activeForces)
        {
            result += force.Intensity * force.Direction;
        }
        return result;
    }

    public void UpdateForces()
    {
        _activeForces.ForEach(x => x.Tick());
        _activeForces = _activeForces.Where(x => x.Intensity > 0).ToList();
    }
    
    public void AddForce(Force force)
    {
        _activeForces.Add(force);
    }
}
