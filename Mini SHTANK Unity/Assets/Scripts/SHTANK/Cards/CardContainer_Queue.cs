using System.Collections.Generic;
using SHTANK.Data.Cards;

namespace SHTANK.Cards
{
    public class CardContainer_Queue : CardContainer
    {
        public Queue<CardObject> CardObjectQueue => _cardObjectQueue;
        
        private readonly Queue<CardObject> _cardObjectQueue = new();
        
        public override void TryAddCard(CardObject cardObject)
        {
            if (_cardLimit && _cardObjectQueue.Count >= _maxCards)
                return;
            
            _cardObjectQueue.Enqueue(cardObject);
            cardObject.SetContainer(this);
        }

        public CardObject TryRemoveCard()
        {
            return _cardObjectQueue.Dequeue();
        }
    }
}