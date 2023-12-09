using UnityEngine;
using Utility.Pooling;

namespace SHTANK.UI
{
    public class CardPreviewTextBoxPoolEventContainer : PoolEventContainer<CardPreviewTextBox>
    {
        public CardPreviewTextBoxPoolEventContainer(CardPreviewTextBox prefab, Transform transform) : base(prefab, transform) { }

        public override CardPreviewTextBox Create()
        {
            return Object.Instantiate(Prefab, Transform);
        }

        public override void OnGet(CardPreviewTextBox toGet)
        {
            toGet.gameObject.SetActive(true);
        }

        public override void OnRelease(CardPreviewTextBox toFree)
        {
            toFree.gameObject.SetActive(false);
        }

        public override void OnDestroy(CardPreviewTextBox toDestroy)
        {
            Object.Destroy(toDestroy.gameObject);
        }
    }
}