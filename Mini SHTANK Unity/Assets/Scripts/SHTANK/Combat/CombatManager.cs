using System;
using SHTANK.GameStates;
using SHTANK.Grid;
using UnityEngine;

namespace SHTANK.Combat
{
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private Transform _combatCenterTransform;

        public static CombatManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void BeginCombat(Vector3 worldPosition)
        {
            if (!GameManager.Instance.TryEnterCombatState())
                return;
            
            _gridManager.InitializeGridForCombat(worldPosition);
            _combatCenterTransform.position = _gridManager.GetCurrentEnemySpaceIntPosition();
        }

        public void EndCombat()
        {
            _gridManager.ClearGridAfterCombat();
        }
    }
}