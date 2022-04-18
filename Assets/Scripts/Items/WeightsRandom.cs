using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items
{
    [Serializable]
    public class WeightsRandom<T>
    {
        [SerializeField] private List<ItemWeight<T>> _weights;

        public T Get(T defaultResult)
        {
            float totalWeight = _weights.Sum(x => x.Weight);
            float random = Random.Range(0, totalWeight);
            float curRandomSum = 0;
            foreach (var weight in _weights)
            {
                curRandomSum += weight.Weight;
                if (random <= curRandomSum)
                    return weight.Item;
            }

            return defaultResult;
        }
    }
}