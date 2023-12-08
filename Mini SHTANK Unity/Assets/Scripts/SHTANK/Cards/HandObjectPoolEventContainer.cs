using UnityEngine;
using UnityEngine.XR;
using Utility.Pooling;

namespace SHTANK.Cards
{
    public class HandObjectPoolEventContainer : PoolEventContainer<HandObject>
    {
        public HandObjectPoolEventContainer(HandObject prefab, Transform transform) : base(prefab, transform) { }

        public override HandObject Create()
        {
            return Object.Instantiate(Prefab, Transform);
        }

        public override void OnGet(HandObject toGet)
        {
            toGet.gameObject.SetActive(true);
        }

        public override void OnRelease(HandObject toFree)
        {
            toFree.gameObject.SetActive(false);
        }

        public override void OnDestroy(HandObject toDestroy)
        {
            Object.Destroy(toDestroy.gameObject);
        }
    }
}