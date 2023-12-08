using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Pool;
using Utility;
using Utility.Pooling;

namespace SHTANK.Grid
{
    public class GridManager : Singleton<GridManager>
    {
        //TODO: add some kind of data per "level" that determines the size of the grid
        [SerializeField] private uint _width;
        [SerializeField] private uint _height;
        [SerializeField] private GridSpaceObject _gridSpaceObjectPrefab;
        [SerializeField] private Transform _gridSpaceObjectParentTransform;
        [Header("Colors")]
        [SerializeField] private Color _enemySpaceColor;
        [SerializeField] private Color _battlefieldColor;
        [SerializeField] private Color _outskirtsColor;
        [SerializeField] private Color _noMansLandColor;
        [Space]
        [Header("Pre-Runtime Data")]
        [ReadOnly] [SerializeField] private List<GridSpaceObject> _gridSpaceObjectList = new();

        private GridSpaceObject _currentEnemySpace;
        
        [Button("Generate Grid")]
        [ContextMenu("Generate Grid")]
        public void GenerateGrid()
        {
            DestroyGrid();
            GridHelper.GenerateGrid(_width, _height, _gridSpaceObjectPrefab, _gridSpaceObjectParentTransform, _gridSpaceObjectList);
        }

        [Button("Destroy Grid")]
        [ContextMenu("Destroy Grid")]
        public void DestroyGrid()
        {
            foreach (GridSpaceObject gridSpaceObject in _gridSpaceObjectList)
            {
                if (gridSpaceObject == null)
                    continue;
                    
                DestroyImmediate(gridSpaceObject.gameObject);
            }
                
            _gridSpaceObjectList.Clear();
        }

        public void InitializeGridForCombat(Vector3 worldPosition)
        {
            if (_gridSpaceObjectList.Count == 0)
            {
                Debug.LogWarning("GridManager: Combat couldn't be started because grid space object list was empty. Does it need to be generated?");
                return;
            }
            
            GridSpaceObject gridSpaceObject = GridHelper.GetClosestGridSpace(worldPosition, _gridSpaceObjectList);
            _currentEnemySpace = gridSpaceObject;
            GridHelper.SetEnemySpaceAndSurroundingTypes(gridSpaceObject);
        }

        public void ClearGridAfterCombat()
        {
            GridHelper.ResetGridSpaceTypes(_gridSpaceObjectList);
        }

        public GridSpaceObject GetCurrentEnemySpace()
        {
            return _currentEnemySpace;
        }

        public Color GetGridSpaceColor(GridSpaceType gridSpaceType)
        {
            switch(gridSpaceType)
            {
                case GridSpaceType.EnemySpace:
                    return _enemySpaceColor;
                case GridSpaceType.Battlefield:
                    return _battlefieldColor;
                case GridSpaceType.Outskirts:
                    return _outskirtsColor;
                case GridSpaceType.None:
                case GridSpaceType.NoMansLand:
                default:
                    return _noMansLandColor;
            }
        }
    }
}