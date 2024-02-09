using System.Collections;
using SHTANK.Combat;
using SHTANK.Data.Cards;

namespace SHTANK.Cards.Processing
{
    public class EffectProcessor_Damage : EffectProcessor
    {
        private readonly CardEffect_Damage _damageInfo;
        private readonly CombatEntity _sourceCombatEntity;
        
        public EffectProcessor_Damage(CardEffect_Damage damageInfo, CombatEntity sourceCombatEntity)
        {
            _damageInfo = damageInfo;
            _sourceCombatEntity = sourceCombatEntity;
        }
        
        public override IEnumerator Process()
        {
            CombatEntity enemy = CombatManager.Instance.StoredEnemy;
            int damage = CombatHelper.CalculateDamage(_damageInfo, _sourceCombatEntity, enemy);
            
            enemy.AdjustHealthByAmount(-damage);

            yield return null;
        }
    }
}