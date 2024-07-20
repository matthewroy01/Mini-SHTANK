using System.Collections.Generic;
using JetBrains.Annotations;
using SHTANK.Combat;
using UnityEngine;
using Utility;

namespace SHTANK.Overworld
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        public List<Enemy> EnemyList => _enemyList;
        public List<Enemy> InCombatEnemyList => _inCombatEnemyList;

        [SerializeField] private CombatManager _combatManager;
        [SerializeField] private CombatResolutionManager _combatResolutionManager;

        private List<Enemy> _enemyList = new();
        private List<Enemy> _inCombatEnemyList = new();

        private void OnEnable()
        {
            _combatManager.BeganCombat += OnBeganCombat;
            _combatResolutionManager.ResolvingCombat += OnResolvingCombat;
        }

        private void OnDisable()
        {
            _combatManager.BeganCombat -= OnBeganCombat;
            _combatResolutionManager.ResolvingCombat -= OnResolvingCombat;
        }

        private void OnBeganCombat(params Enemy[] enemyArray)
        {
            _inCombatEnemyList = new List<Enemy>(enemyArray);
        }

        private void OnResolvingCombat(CombatResolutionInfo combatResolutionInfo)
        {
            QueueEnemiesForKilling(combatResolutionInfo.InvolvedEnemyArray);
        }

        public void Initialize(List<Enemy> enemyList)
        {
            _enemyList = enemyList;
        }

        public void ToggleEnemies(bool enable)
        {
            foreach (Enemy enemy in _enemyList)
            {
                enemy.gameObject.SetActive(enable);
            }
        }

        public void TryKillEnemies()
        {
            for (int i = 0; i < _enemyList.Count; ++i)
            {
                if (!_enemyList[i].TryKill())
                    continue;

                _enemyList.RemoveAt(i);
                --i;
            }
        }

        private void QueueEnemiesForKilling(CombatEntity[] combatEntityArray)
        {
            foreach (CombatEntity combatEntity in combatEntityArray)
            {
                Enemy derivedEnemy = combatEntity.DerivedEnemy;
                if (derivedEnemy == null)
                    continue;

                combatEntity.DerivedEnemy.MarkAsDead();
            }
        }
    }
}