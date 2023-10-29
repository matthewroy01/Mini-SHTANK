using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utility
{
    public class Singleton<T> : MonoBehaviour where T : Object
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (FindObjectsOfType<T>().Length > 1)
            {
                Destroy(gameObject);
            }

            Instance = this as T;
            
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
    }
}