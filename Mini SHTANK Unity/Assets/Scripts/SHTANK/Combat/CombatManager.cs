using System;
using System.Collections.Generic;
using SHTANK.Data.CombatEntities;
using SHTANK.GameStates;
using SHTANK.Grid;
using SHTANK.Overworld;
using UnityEngine;
using UnityEngine.Pool;
using Utility;

namespace SHTANK.Combat
{
    public class CombatManager : Singleton<CombatManager>
    {
        public event Action<Enemy[]> BeganCombat;

        public CombatEntity StoredEnemy => _storedEnemy;
        public List<CombatEntity> StoredPlayers => _storedPlayers;
        public float CurrentDamageMultiplier => _currentDamageMultiplier;

        [SerializeField] private GridManager _gridManager;
        [SerializeField] private Transform _combatCenterTransform;
        [SerializeField] private CombatEntity _combatEntityPrefab;
        [Header("Placeholder")]
        [SerializeField] private List<CombatEntityDefinition> _playersToSpawn = new();

        private ObjectPool<CombatEntity> _combatEntityPool;
        private CombatEntityPoolEventContainer _combatEntityPoolEventContainer;
        private CombatEntity _tempCombatEntity;
        private Vector3 _spawnPosition;
        private CombatEntity _storedEnemy;
        private readonly List<CombatEntity> _storedPlayers = new();
        private float _currentDamageMultiplier = 1.0f;

        protected override void Awake()
        {
            base.Awake();

            _combatEntityPoolEventContainer = new CombatEntityPoolEventContainer(_combatEntityPrefab, transform);
            _combatEntityPool = new ObjectPool<CombatEntity>(_combatEntityPoolEventContainer.Create,
                _combatEntityPoolEventContainer.OnGet,
                _combatEntityPoolEventContainer.OnRelease,
                _combatEntityPoolEventContainer.OnDestroy);
        }

        public void BeginCombat(Enemy enemy, Vector3 worldPosition)
        {
            if (!GameManager.Instance.TryEnterCombatState())
                return;

            _gridManager.InitializeGridForCombat(worldPosition);
            GridSpaceObject enemyGridSpaceObject = _gridManager.GetCurrentEnemySpace();
            _combatCenterTransform.position = enemyGridSpaceObject.IntWorldPosition;

            CreateEnemyCombatEntity(enemy);

            CreatePlayerCombatEntities();

            PositionEnemyCombatEntity(enemyGridSpaceObject);
            PositionPlayerCombatEntities(enemyGridSpaceObject);

            Enemy[] enemyArray = new Enemy[] { enemy };
            BeganCombat?.Invoke(enemyArray);
        }

        private void CreateEnemyCombatEntity(Enemy enemy)
        {
            _tempCombatEntity = _combatEntityPool.Get();
            _tempCombatEntity.Initialize(enemy.CombatEntityDefinition, enemy);

            _storedEnemy = _tempCombatEntity;
        }

        private void CreatePlayerCombatEntities()
        {
            _storedPlayers.Clear();

            foreach (CombatEntityDefinition combatEntityDefinition in _playersToSpawn)
            {
                _tempCombatEntity = _combatEntityPool.Get();
                _tempCombatEntity.Initialize(combatEntityDefinition);

                _storedPlayers.Add(_tempCombatEntity);
            }
        }

        private void PositionEnemyCombatEntity(GridSpaceObject enemyGridSpaceObject)
        {
            _storedEnemy.transform.position = enemyGridSpaceObject.transform.position;
        }

        private void PositionPlayerCombatEntities(GridSpaceObject enemyGridSpaceObject)
        {
            GridConnections gridConnections = enemyGridSpaceObject.GridConnections;

            for (int i = 0; i < _storedPlayers.Count; ++i)
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

                _storedPlayers[i].transform.position = _spawnPosition;
            }
        }

        public bool GetEnemyHealthIsZero()
        {
            return _storedEnemy.CurrentHealth <= 0;
        }

        public void EndCombat()
        {
            _gridManager.ClearGridAfterCombat();

            ClearCombatEntities();
        }

        private void ClearCombatEntities()
        {
            foreach (CombatEntity combatEntity in _storedPlayers)
            {
                _combatEntityPool.Release(combatEntity);
            }

            _combatEntityPool.Release(_storedEnemy);
        }
    }
}