using UnityEngine;
using Utility.Pooling;

namespace SHTANK.Cards
{
    public class DeckObjectPoolEventContainer : PoolEventContainer<DeckObject>
    {
        public DeckObjectPoolEventContainer(DeckObject prefab, Transform transform) : base(prefab, transform) { }

        public override DeckObject Create()
        {
            return Object.Instantiate(Prefab, Transform);
        }

        public override void OnGet(DeckObject toGet)
        {
            toGet.gameObject.SetActive(true);
        }

        public override void OnRelease(DeckObject toFree)
        {
            toFree.gameObject.SetActive(false);
        }

        public override void OnDestroy(DeckObject toDestroy)
        {
            Object.Destroy(toDestroy.gameObject);
        }
    }
}