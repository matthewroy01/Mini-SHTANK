﻿using System.Collections.Generic;
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

        public static List<T> ShuffleList<T>(List<T> list)
        {
            List<T> tmpList = new();
            tmpList.AddRange(list);

            List<T> result = new();

            while (tmpList.Count > 0)
            {
                result.Add(tmpList[Random.Range(0, tmpList.Count)]);
            }

            return result;
        }
    }
}