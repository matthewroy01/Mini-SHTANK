using SHTANK.Data.Cards;

namespace SHTANK.Combat
{
    public static class CombatHelper
    {
        public static int CalculateDamage(CardEffect_Damage damageInfo, CombatEntity attacker, CombatEntity defender)
        {
            int damage = damageInfo.Damage;

            damage += attacker.CombatEntityDefinition.Attack;
            // TODO: include any buff or debuff effects the attacker might have
            damage = (int)(damage * CombatManager.Instance.CurrentDamageMultiplier);
            damage -= defender.CombatEntityDefinition.Defense;
            // TODO: include any buff or debuff effects the defender might have

            return damage;
        }
    }
}