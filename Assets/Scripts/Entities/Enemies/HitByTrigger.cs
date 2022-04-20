using System.Collections;
using System.Collections.Generic;
using Entities.HitBoxes;
using UnityEngine;

namespace Entities.Enemies
{
    public class HitByTrigger : MonoBehaviour
    {
        private readonly List<HitBox> _damagedHitBox = new List<HitBox>();
        private readonly List<HitBox> _overlapHitBoxes = new List<HitBox>();
        private List<HitBoxType> _targetTypes;
        private HitData _hitData;
        private float _reload;


        public void Init(HitData hitData, List<HitBoxType> targetTypes, float reload = 1f)
        {
            _reload = reload;
            _targetTypes = targetTypes;
            _hitData = hitData;
        }

        private void Update()
        {
            foreach (var overlapHitBox in _overlapHitBoxes)
            {
                if (!overlapHitBox)
                {
                    _overlapHitBoxes.Remove(overlapHitBox);
                    continue;
                }

                if (_damagedHitBox.Contains(overlapHitBox))
                    continue;

                overlapHitBox.TryHit(_hitData, transform, _targetTypes);
                _damagedHitBox.Add(overlapHitBox);
                StartCoroutine(ReloadForHitBox(overlapHitBox, _reload));
            }
        }

        private IEnumerator ReloadForHitBox(HitBox hitBox, float time)
        {
            yield return new WaitForSeconds(time);
            _damagedHitBox.Remove(hitBox);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out HitBox hitBox)) 
                _overlapHitBoxes.Add(hitBox);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out HitBox hitBox)) 
                _overlapHitBoxes.Remove(hitBox);
        }
    }
}
