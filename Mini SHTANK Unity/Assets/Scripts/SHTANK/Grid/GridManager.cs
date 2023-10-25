using UnityEngine;
using UnityEngine.Pool;
using Utility.Pooling;

namespace SHTANK.Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GridSpaceObject _gridSpaceObjectPrefab;
        [SerializeField] private Transform _gridSpaceObjectParentTransform;
        
        private GridContainer _gridContainer;
        private ObjectPool<GridSpaceObject> _gridSpaceObjectPool;
        private GridSpaceObjectPoolEventContainer _gridSpaceObjectPoolEventContainer;

        private void Awake()
        {
            _gridSpaceObjectPoolEventContainer = new GridSpaceObjectPoolEventContainer(_gridSpaceObjectPrefab, _gridSpaceObjectParentTransform);
            _gridSpaceObjectPool = PoolHelper<GridSpaceObject>.CreatePool(_gridSpaceObjectPoolEventContainer);
            
            _gridContainer = new GridContainer(20, 20, _gridSpaceObjectPool);
        }
    }
}