using NaughtyAttributes;
using SHTANK.Data.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace SHTANK.Cards
{
    public abstract class CardContainer : MonoBehaviour
    {
        public bool CardLimit => _cardLimit;
        public int MaxCards => _maxCards;

        [SerializeField] protected bool _cardLimit;
        [ShowIf("_cardLimit")]
        [SerializeField] protected int _maxCards;
        [SerializeField] protected RectTransform _layoutGroupRectTransform;

        public abstract void TryAddCard(CardObject cardObject);

        private void RefreshLayoutGroup()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutGroupRectTransform);
        }

        protected void SetParent(CardObject cardObject)
        {
            cardObject.transform.SetParent(_layoutGroupRectTransform);
        }
    }
}