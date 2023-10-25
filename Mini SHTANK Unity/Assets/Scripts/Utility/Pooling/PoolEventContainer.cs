using UnityEngine;
using UnityEngine.Pool;

namespace Utility.Pooling
{
    public abstract class PoolEventContainer<T> where T : class
    {
        protected T Prefab { get; private set; }
        protected Transform Transform { get; private set; }

        public PoolEventContainer(T prefab, Transform transform)
        {
            Prefab = prefab;
            Transform = transform;
        }

        public abstract T Create();
        public abstract void OnGet(T toGet);
        public abstract void OnRelease(T toFree);
        public abstract void OnDestroy(T toDestroy);
    }
}