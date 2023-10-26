using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Utility;

namespace SHTANK.Grid
{
    public class GridContainer
    {
        public GridSpace[,] GridSpaceArray => _gridSpaceArray;
        
        private readonly GridSpace[,] _gridSpaceArray;
        private Vector3 _spawnPosition;
        private List<GridSpace> _visitedList = new();

        public GridContainer(uint width, uint height, ObjectPool<GridSpaceObject> objectPool)
        {
            _gridSpaceArray = new GridSpace[width, height];
            
            _spawnPosition.y = 0.0f;

            int x, y;
            string id = "";
            
            _PopulateArray();
            _SetConnections();
            
            void _PopulateArray()
            {
                for (x = 0; x < _gridSpaceArray.GetLength(0); ++x)
                {
                    for (y = 0; y < _gridSpaceArray.GetLength(1); ++y)
                    {
                        id = "(" + x + ", " + y + ")";
                        
                        GridSpaceObject gridSpaceObject = _CreateGridSpaceObject();
                        _spawnPosition.x = x;
                        _spawnPosition.z = y;
                        gridSpaceObject.transform.position = _spawnPosition;
                        
                        _gridSpaceArray[x, y] = new GridSpace(id, gridSpaceObject);
                    }
                }
            }
            
            void _SetConnections()
            {
                for (x = 0; x < _gridSpaceArray.GetLength(0); ++x)
                {
                    for (y = 0; y < _gridSpaceArray.GetLength(1); ++y)
                    {
                        GridSpace up = GridHelper.TryGetObjectFrom2DArray(_gridSpaceArray, x, y + 1);
                        GridSpace down = GridHelper.TryGetObjectFrom2DArray(_gridSpaceArray, x, y - 1);
                        GridSpace left = GridHelper.TryGetObjectFrom2DArray(_gridSpaceArray, x - 1, y);
                        GridSpace right = GridHelper.TryGetObjectFrom2DArray(_gridSpaceArray, x + 1, y);
                    
                        GridSpace upLeft = GridHelper.TryGetObjectFrom2DArray(_gridSpaceArray, x - 1, y + 1);
                        GridSpace upRight = GridHelper.TryGetObjectFrom2DArray(_gridSpaceArray, x + 1, y + 1);
                        GridSpace downLeft = GridHelper.TryGetObjectFrom2DArray(_gridSpaceArray, x - 1, y - 1);
                        GridSpace downRight = GridHelper.TryGetObjectFrom2DArray(_gridSpaceArray, x + 1, y - 1);
                    
                        _gridSpaceArray[x, y].SetConnections(up, down, left, right, upLeft, upRight, downLeft, downRight);
                    }
                }
            }

            GridSpaceObject _CreateGridSpaceObject()
            {
                GridSpaceObject tmp = objectPool.Get();
                tmp.gameObject.name = "GridSpaceObject " + id;

                return tmp;
            }
        }

        public GridSpace GetClosestGridSpace(Vector3 worldPosition)
        {
            Vector3Int intWorldPosition = VectorHelper.RoundVector3(worldPosition);
            intWorldPosition.y = 0;

            foreach (GridSpace gridSpace in _gridSpaceArray)
            {
                if (gridSpace.IntWorldPosition == intWorldPosition)
                {
                    return gridSpace;
                }
            }
            
            Debug.LogWarning("GridContainer: Provided world position was not found on the grid. Are you off-grid?");
            return null;
        }

        public void SetEnemySpaceAndSurroundingTypes(GridSpace enemySpace)
        {
            _visitedList.Clear();

            // enemy space
            _SetGridSpaceType(GridSpaceType.EnemySpace,
                true,
                enemySpace);
            
            // battlefield
            GridConnections enemySpaceGridConnections = enemySpace.GridConnections;
            _SetGridSpaceType(GridSpaceType.Battlefield,
                true,
                enemySpaceGridConnections.Up.GridSpace,
                enemySpaceGridConnections.Down.GridSpace,
                enemySpaceGridConnections.Left.GridSpace,
                enemySpaceGridConnections.Right.GridSpace,
                enemySpaceGridConnections.UpLeft.GridSpace,
                enemySpaceGridConnections.UpRight.GridSpace,
                enemySpaceGridConnections.DownLeft.GridSpace,
                enemySpaceGridConnections.DownRight.GridSpace);

            // outskirts
            foreach (GridSpace battlefieldGridSpace in _visitedList)
            {
                GridConnections battlefieldConnections = battlefieldGridSpace.GridConnections;
                _SetGridSpaceType(GridSpaceType.Outskirts,
                    false,
                    battlefieldConnections.Up.GridSpace,
                    battlefieldConnections.Down.GridSpace,
                    battlefieldConnections.Left.GridSpace,
                    battlefieldConnections.Right.GridSpace,
                    battlefieldConnections.UpLeft.GridSpace,
                    battlefieldConnections.UpRight.GridSpace,
                    battlefieldConnections.DownLeft.GridSpace,
                    battlefieldConnections.DownRight.GridSpace);
            }
            
            enemySpace.GridConnections.Up.GridSpace.SetType(GridSpaceType.Battlefield);

            void _SetGridSpaceType(GridSpaceType gridSpaceType, bool addToList, params GridSpace[] gridSpaceArray)
            {
                foreach (GridSpace gridSpace in gridSpaceArray)
                {
                    if (_visitedList.Contains(gridSpace))
                        continue;
                    
                    gridSpace.SetType(gridSpaceType);

                    if (!addToList)
                        continue;
                    
                    _visitedList.Add(gridSpace);
                }
            }
        }
        
        public void ResetGridSpaceTypes()
        {
            
        }
    }
}