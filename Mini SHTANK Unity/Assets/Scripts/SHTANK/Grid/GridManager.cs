using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Utility.Pooling;

namespace SHTANK.Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GridSpaceObject _gridSpaceObjectPrefab;
        [SerializeField] private Transform _gridSpaceObjectParentTransform;
        
        //TODO: add some kind of data per "level" that determines the size of the grid
        
        private GridContainer _gridContainer;
        private ObjectPool<GridSpaceObject> _gridSpaceObjectPool;
        private GridSpaceObjectPoolEventContainer _gridSpaceObjectPoolEventContainer;

        private void Awake()
        {
            _gridSpaceObjectPoolEventContainer = new GridSpaceObjectPoolEventContainer(_gridSpaceObjectPrefab, _gridSpaceObjectParentTransform);
            _gridSpaceObjectPool = PoolHelper<GridSpaceObject>.CreatePool(_gridSpaceObjectPoolEventContainer);
            
            _gridContainer = new GridContainer(20, 20, _gridSpaceObjectPool);
        }

        public void InitializeGridForCombat(Vector3 worldPosition)
        {
            GridSpace enemySpace = _gridContainer.GetClosestGridSpace(worldPosition);
            _gridContainer.SetEnemySpaceAndSurroundingTypes(enemySpace);
        }

        public void ClearGridAfterCombat()
        {
            _gridContainer.ResetGridSpaceTypes();
        }
    }
}