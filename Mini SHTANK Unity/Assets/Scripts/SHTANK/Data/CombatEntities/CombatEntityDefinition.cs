using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace SHTANK.Data.CombatEntities
{
    [CreateAssetMenu(fileName = "New Combat Entity Definition", menuName = "SHTANK/CombatEntityDefinition", order = 0)]
    public class CombatEntityDefinition : ScriptableObject
    {
        public CombatEntityType CombatEntityType => _combatEntityType;
        
        public string EntityName => _entityName;

        public int Health => _health;
        public int Attack => _attack;
        public int Defense => _defense;
        public int ComboBonus => _comboBonus;
        public int Metabolism => _metabolism;
        public int Speed => _speed;

        [SerializeField] private CombatEntityType _combatEntityType;
        
        [Header("Info")]
        [SerializeField] private string _entityName = "Default Entity Type";
        [TextArea(3, 10)]
        [SerializeField] private string _entityDescription = "Default Entity Description";

        [Header("Stats")]
        [SerializeField] private int _health = 1;
        [SerializeField] private int _attack;
        [SerializeField] private int _defense;
        [ShowIf("_combatEntityType", CombatEntityType.Player)]
        [SerializeField] private int _comboBonus;
        [SerializeField] private int _metabolism;
        [ShowIf("_combatEntityType", CombatEntityType.Player)]
        [SerializeField] private int _speed;
        [ShowIf("_combatEntityType", CombatEntityType.Enemy)]
        [SerializeField] private int _speedRating;
        [ShowIf("_combatEntityType", CombatEntityType.Player)]
        [SerializeField] private int _movement = 2;
        
        [Header("Growth Rates")]
        [Range(0.0f, 100.0f)]
        [SerializeField] private int _growthRateHealth;
        [Range(0.0f, 100.0f)]
        [SerializeField] private int _growthRateAttack;
        [Range(0.0f, 100.0f)]
        [SerializeField] private int _growthRateDefense;
        [Range(0.0f, 100.0f), ShowIf("_combatEntityType", CombatEntityType.Player)]
        [SerializeField] private int _growthRateComboBonus;
        [Range(0.0f, 100.0f)]
        [SerializeField] private int _growthRateMetabolism;
        [Range(0.0f, 100.0f), ShowIf("_combatEntityType", CombatEntityType.Player)]
        [SerializeField] private int _growthRateSpeed;
        [Range(0.0f, 100.0f), ShowIf("_combatEntityType", CombatEntityType.Enemy)]
        [SerializeField] private int _growthRateSpeedRating;
    }
}