using System.Collections.Generic;
using SHTANK.Combat;

namespace SHTANK.Cards
{
    public class QueuedCardInfo
    {
        public CardObject CardObject => _cardObject;
        public CombatEntity SourceCombatEntity => _sourceCombatEntity;
        public CombatEntity[] TargetCombatEntityArray => _targetCombatEntityArray;
        
        private readonly CardObject _cardObject;
        private readonly CombatEntity _sourceCombatEntity;
        private readonly CombatEntity[] _targetCombatEntityArray;
        
        public QueuedCardInfo(CardObject cardObject, CombatEntity sourceCombatEntity, params CombatEntity[] targetCombatEntityArray)
        {
            _cardObject = cardObject;
            _sourceCombatEntity = sourceCombatEntity;
            _targetCombatEntityArray = targetCombatEntityArray;
        }
    }
}