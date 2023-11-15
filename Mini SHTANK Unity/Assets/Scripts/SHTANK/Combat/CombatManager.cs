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
        [SerializeField] private List<CombatEntityDefinition> _playersToSpawn = new();

        private ObjectPool<CombatEntity> _combatEntityPool;
        private CombatEntityPoolEventContainer _combatEntityPoolEventContainer;
        private CombatEntity _tempCombatEntity;
        private Vector3 _spawnPosition;
        private CombatEntity _enemyStored;
        private readonly List<CombatEntity> _playersStored = new();

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
            GridSpaceObject enemyGridSpaceObject = _gridManager.GetCurrentEnemySpace();
            _combatCenterTransform.position = enemyGridSpaceObject.IntWorldPosition;

            CreateEnemyCombatEntity(enemyCombatEntityDefinition);
            CreatePlayerCombatEntities();

            PositionEnemyCombatEntity(enemyGridSpaceObject);
            PositionPlayerCombatEntities(enemyGridSpaceObject);
        }
        
        private void CreateEnemyCombatEntity(CombatEntityDefinition enemyCombatEntityDefinition)
        {
            _tempCombatEntity = _combatEntityPool.Get();
            _tempCombatEntity.Initialize(enemyCombatEntityDefinition);

            _enemyStored = _tempCombatEntity;
        }

        private void CreatePlayerCombatEntities()
        {
            _playersStored.Clear();
            
            foreach (CombatEntityDefinition combatEntityDefinition in _playersToSpawn)
            {
                _tempCombatEntity = _combatEntityPool.Get();
                _tempCombatEntity.Initialize(combatEntityDefinition);
                
                _playersStored.Add(_tempCombatEntity);
            }
        }

        private void PositionEnemyCombatEntity(GridSpaceObject enemyGridSpaceObject)
        {
            _enemyStored.transform.position = enemyGridSpaceObject.transform.position;
        }

        private void PositionPlayerCombatEntities(GridSpaceObject enemyGridSpaceObject)
        {
            GridConnections gridConnections = enemyGridSpaceObject.GridConnections;
            
            for (int i = 0; i < _playersStored.Count; ++i)
            {
                switch (i)
                {
                    case 0:
                        _spawnPosition = gridConnections.UpLeft.GridSpaceObject.transform.position;
                        break;
                    case 1:
                        _spawnPosition = gridConnections.Left.GridSpaceObject.transform.position;
                        break;
                    case 2:
                        _spawnPosition = gridConnections.DownLeft.GridSpaceObject.transform.position;
                        break;
                    case 3:
                        _spawnPosition = gridConnections.Up.GridSpaceObject.transform.position;
                        break;
                    case 4:
                        _spawnPosition = gridConnections.Down.GridSpaceObject.transform.position;
                        break;
                    case 5:
                        _spawnPosition = gridConnections.UpRight.GridSpaceObject.transform.position;
                        break;
                    case 6:
                        _spawnPosition = gridConnections.Right.GridSpaceObject.transform.position;
                        break;
                    case 7:
                        _spawnPosition = gridConnections.DownRight.GridSpaceObject.transform.position;
                        break;
                    default:
                        return;
                }

                _playersStored[i].transform.position = _spawnPosition;
            }
        }

        public void EndCombat()
        {
            _gridManager.ClearGridAfterCombat();
        }
    }
}