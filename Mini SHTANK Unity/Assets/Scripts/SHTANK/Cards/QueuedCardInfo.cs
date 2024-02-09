using System.Collections.Generic;
using SHTANK.Combat;

namespace SHTANK.Cards
{
    public class QueuedCardInfo
    {
        public CardObject CardObject => _cardObject;
        public CombatEntity SourceCombatEntity => _sourceCombatEntity;
        
        private readonly CardObject _cardObject;
        private readonly CombatEntity _sourceCombatEntity;
        
        public QueuedCardInfo(CardObject cardObject, CombatEntity sourceCombatEntity)
        {
            _cardObject = cardObject;
            _sourceCombatEntity = sourceCombatEntity;
        }
    }
}