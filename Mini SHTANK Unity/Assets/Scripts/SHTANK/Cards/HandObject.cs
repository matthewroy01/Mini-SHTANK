using SHTANK.Combat;

namespace SHTANK.Cards
{
    public class HandObject : CardContainer_List
    {
        public CombatEntity AssociatedCombatEntity => _associatedCombatEntity;
        public bool CardsEnabled => _cardsEnabled;

        private CombatEntity _associatedCombatEntity;
        private CardObject _playedCard;
        private bool _cardsEnabled = true;

        public void Initialize(CombatEntity associatedCombatEntity)
        {
            _associatedCombatEntity = associatedCombatEntity;
        }

        public void PlayCard(CardObject cardObject)
        {
            if (!CardObjectList.Contains(cardObject))
                return;

            _playedCard = cardObject;
            
            DisableCards();
        }

        public void CancelPlayedCard()
        {
            _playedCard = null;
            
            EnableCards();
        }

        public CardObject RemovePlayedCard()
        {
            EnableCards();

            return TryRemoveCard(_playedCard);
        }

        private void DisableCards()
        {
            _cardsEnabled = false;
            
            foreach (CardObject cardObject in CardObjectList)
            {
                if (cardObject == _playedCard)
                    continue;

                cardObject.GrayOut();
            }
        }

        private void EnableCards()
        {
            _cardsEnabled = true;
            
            foreach (CardObject cardObject in CardObjectList)
            {
                cardObject.RestoreColor();
            }
        }
    }
}