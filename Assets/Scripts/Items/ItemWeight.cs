using System;
using UnityEngine.Serialization;

namespace Items
{
    [Serializable]
    public class ItemWeight<T>
    {
        public T Item;
        public float Weight;
    }
}