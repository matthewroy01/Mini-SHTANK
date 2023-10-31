using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class RandomHelper
    {
        public static T GetRandomItem<T>(List<T> list)
        {
            if (list.Count != 0)
                return list[Random.Range(0, list.Count)];
            
            Debug.LogWarning("RandomHelper: provided list was empty. Returning default.");
            return default;
        }
    }
}