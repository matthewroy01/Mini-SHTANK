using UnityEngine;
using UnityEngine.Pool;

namespace Utility.Pooling
{
    public static class PoolHelper<T> where T : class
    {
        public static ObjectPool<T> CreatePool(PoolEventContainer<T> poolEventContainer)
        {
            return new ObjectPool<T>(poolEventContainer.Create, poolEventContainer.OnGet, poolEventContainer.OnRelease, poolEventContainer.OnDestroy);
        }
    }
}