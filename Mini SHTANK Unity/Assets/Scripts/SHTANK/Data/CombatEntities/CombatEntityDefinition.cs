using NaughtyAttributes;
using SHTANK.Combat;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace SHTANK.Data.CombatEntities
{
    [CreateAssetMenu(fileName = "New Combat Entity Definition", menuName = "SHTANK/Combat Entity Definition", order = 0)]
    public class CombatEntityDefinition : ScriptableObject
    {
        public CombatEntityType CombatEntityType => _combatEntityType;
        
        public string EntityName => _entityName;
        public string EntityDescription => _entityDescription;
        public Sprite Sprite => _sprite;

        public int Health => _health;
        public int Attack => _attack;
        public int Defense => _defense;
        public int ComboBonus => _comboBonus;
        public int Metabolism => _metabolism;
        public int Speed => _speed;
        public int Movement => _movement;
        public int ExperienceAwarded => _experienceAwarded;

        [SerializeField] private CombatEntityType _combatEntityType;
        
        [Header("Info")]
        [SerializeField] private string _entityName = "Default Entity Card";
        [TextArea(3, 10)]
        [SerializeField] private string _entityDescription = "Default Entity Description";
        [ShowAssetPreview]
        [SerializeField] private Sprite _sprite;

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
        [ShowIf("_combatEntityType", CombatEntityType.Enemy)]
        [SerializeField] private int _experienceAwarded;
        
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