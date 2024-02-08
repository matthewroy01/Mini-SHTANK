using UnityEngine;
using Utility.Pooling;

namespace SHTANK.Cards
{
    public class CardObjectPoolEventContainer : PoolEventContainer<CardObject>
    {
        public CardObjectPoolEventContainer(CardObject prefab, Transform transform) : base(prefab, transform) { }

        public override CardObject Create()
        {
            return Object.Instantiate(Prefab, Transform);
        }

        public override void OnGet(CardObject toGet)
        {
            toGet.transform.SetSiblingIndex(int.MaxValue);
            toGet.gameObject.SetActive(true);
        }

        public override void OnRelease(CardObject toFree)
        {
            toFree.gameObject.SetActive(false);
        }

        public override void OnDestroy(CardObject toDestroy)
        {
            Object.Destroy(toDestroy.gameObject);
        }
    }
}