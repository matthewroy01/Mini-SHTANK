using System.Collections.Generic;
using SHTANK.Data.CombatEntities;
using SHTANK.GameStates;
using SHTANK.Grid;
using UnityEngine;
using UnityEngine.Pool;
using Utility;

namespace SHTANK.Combat
{
    public class CombatManager : Singleton<CombatManager>
    {
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private Transform _combatCenterTransform;
        [SerializeField] private CombatEntity _combatEntityPrefab;
        [Header("Placeholder")]
        [SerializeField] private List<CombatEntityDefinition> _playerCombatEntities = new();

        private ObjectPool<CombatEntity> _combatEntityPool;
        private CombatEntityPoolEventContainer _combatEntityPoolEventContainer;
        private CombatEntity _tempCombatEntity;

        protected override void Awake()
        {
            base.Awake();
            
            _combatEntityPoolEventContainer = new CombatEntityPoolEventContainer(_combatEntityPrefab, transform);
            _combatEntityPool = new ObjectPool<CombatEntity>(_combatEntityPoolEventContainer.Create,
                _combatEntityPoolEventContainer.OnGet,
                _combatEntityPoolEventContainer.OnRelease,
                _combatEntityPoolEventContainer.OnDestroy);
        }

        public void BeginCombat(CombatEntityDefinition enemyCombatEntityDefinition, Vector3 worldPosition)
        {
            if (!GameManager.Instance.TryEnterCombatState())
                return;
            
            _gridManager.InitializeGridForCombat(worldPosition);
            _combatCenterTransform.position = _gridManager.GetCurrentEnemySpaceIntPosition();

            CreateEnemyCombatEntity(enemyCombatEntityDefinition);
            CreatePlayerCombatEntities();
            
            // TODO: place combat entities on the grid
        }
        
        private void CreateEnemyCombatEntity(CombatEntityDefinition enemyCombatEntityDefinition)
        {
            _tempCombatEntity = _combatEntityPool.Get();
            _tempCombatEntity.Initialize(enemyCombatEntityDefinition);
        }

        private void CreatePlayerCombatEntities()
        {
            foreach (CombatEntityDefinition combatEntityDefinition in _playerCombatEntities)
            {
                _tempCombatEntity = _combatEntityPool.Get();
                _tempCombatEntity.Initialize(combatEntityDefinition);
            }
        }

        public void EndCombat()
        {
            _gridManager.ClearGridAfterCombat();
        }
    }
}