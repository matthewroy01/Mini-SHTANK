using System.Collections.Generic;
using SHTANK.Data.Cards;

namespace SHTANK.Cards
{
    public class CardContainer_List : CardContainer
    {
        public List<CardObject> CardObjectList => _cardObjectList;

        private readonly List<CardObject> _cardObjectList = new();
        
        public override void TryAddCard(CardObject cardObject)
        {
            if (_cardLimit && _cardObjectList.Count >= _maxCards)
                return;
            
            _cardObjectList.Add(cardObject);
            cardObject.SetContainer(this);
            SetParent(cardObject);
        }

        public CardObject TryRemoveCard(CardObject cardObject)
        {
            if (cardObject == null)
                return null;
            
            if (!_cardObjectList.Contains(cardObject))
                return null;
            
            _cardObjectList.Remove(cardObject);
            return cardObject;
        }
    }
}