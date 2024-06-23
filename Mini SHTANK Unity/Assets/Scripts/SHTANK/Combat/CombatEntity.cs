using System;
using NaughtyAttributes;
using SHTANK.Data.CombatEntities;
using SHTANK.Overworld;
using UnityEngine;

namespace SHTANK.Combat
{
    public class CombatEntity : MonoBehaviour
    {
        public event Action TookDamage;
        public event Action TookZeroDamage;
        public event Action Healed;
        public event Action ReachedZeroHealth;

        public CombatEntityDefinition CombatEntityDefinition => _combatEntityDefinition;
        public int CurrentHealth => _currentHealth;
        public Enemy DerivedEnemy => _derivedEnemy;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] [ReadOnly] private int _currentHealth;

        private CombatEntityDefinition _combatEntityDefinition;
        private Enemy _derivedEnemy;
        private int _maxHealth;

        public void Initialize(CombatEntityDefinition combatEntityDefinition, Enemy derivedEnemy = null)
        {
            _combatEntityDefinition = combatEntityDefinition;
            _derivedEnemy = derivedEnemy;
            gameObject.name = "CombatEntity (" + _combatEntityDefinition.EntityName + ")";

            SetStats();

            UpdateVisuals();
        }

        private void SetStats()
        {
            _maxHealth = _combatEntityDefinition.Health;
            _currentHealth = _maxHealth;
        }

        private void UpdateVisuals()
        {
            if (_combatEntityDefinition.Sprite == null)
                return;

            _spriteRenderer.sprite = _combatEntityDefinition.Sprite;
        }

        public void AdjustHealthByAmount(int amount)
        {
            if (amount > 0)
            {
                _currentHealth += amount;
                Healed?.Invoke();
                return;
            }

            if (amount == 0)
            {
                TookZeroDamage?.Invoke();
                return;
            }

            if (_currentHealth + amount <= 0)
            {
                _currentHealth = 0;
                ReachedZeroHealth?.Invoke();
                return;
            }

            _currentHealth += amount;
            TookDamage?.Invoke();
        }
    }
}