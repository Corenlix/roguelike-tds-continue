using UnityEngine;

namespace LevelGeneration
{
    public class NormalRandom
    {
        public static float Range(float minValue = 0.0f, float maxValue = 1.0f)
        {
            float u, v, S;
 
            do
            {
                u = 2.0f * Random.value - 1.0f;
                v = 2.0f * Random.value - 1.0f;
                S = u * u + v * v;
            }
            while (S >= 1.0f);
 
            // Standard Normal Distribution
            float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
 
            // Normal Distribution centered between the min and max value
            // and clamped following the "three-sigma rule"
            float mean = (minValue + maxValue) / 2.0f;
            float sigma = (maxValue - mean) / 3.0f;
            return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
        }
    
        public static int Range(int minValue = 0, int maxValue = 1)
        {
            return Mathf.RoundToInt(Range((float)minValue, (float)maxValue));
        }
    }
}