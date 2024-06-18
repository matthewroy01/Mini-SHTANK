using System.Collections;
using SHTANK.Combat;
using SHTANK.Data.Cards;

namespace SHTANK.Cards.Processing
{
    public class EffectProcessor_Damage : EffectProcessor
    {
        private readonly CardEffect_Damage _damageInfo;
        private readonly CombatEntity _sourceCombatEntity;
        private readonly CombatEntity[] _targetCombatEntityArray;
        
        public EffectProcessor_Damage(CardEffect_Damage damageInfo, CombatEntity sourceCombatEntity, params CombatEntity[] targetCombatEntityArray)
        {
            _damageInfo = damageInfo;
            _sourceCombatEntity = sourceCombatEntity;
            _targetCombatEntityArray = targetCombatEntityArray;
        }
        
        public override IEnumerator Process()
        {
            foreach (CombatEntity combatEntity in _targetCombatEntityArray)
            {
                int damage = CombatHelper.CalculateDamage(_damageInfo, _sourceCombatEntity, combatEntity);
            
                combatEntity.AdjustHealthByAmount(-damage);   
            }

            yield return null;
        }
    }
}