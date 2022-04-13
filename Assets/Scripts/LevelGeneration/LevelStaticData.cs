using System;
using Infrastructure.StaticData;
using UnityEngine;

namespace LevelGeneration
{
    public abstract class LevelStaticData : ScriptableObject
    {
        public LevelId LevelId;
        public abstract Level Generate();
    }
}