using System.Collections.Generic;
using SHTANK.Data.Cards;
using Utility;

namespace SHTANK.Cards
{
    public class DeckObject : CardContainer_Queue
    {
        private readonly Queue<CardDefinition> _cards = new();

        public void Initialize(List<DeckCardInfo> _deckCardInfoList)
        {
            _deckCardInfoList = RandomHelper.ShuffleList(_deckCardInfoList);
            
            foreach (DeckCardInfo deckCardInfo in _deckCardInfoList)
            {
                for (uint i = 0; i < deckCardInfo.Amount; ++i)
                {
                    _cards.Enqueue(deckCardInfo.CardDefinition);
                }
            }
        }
    }
}