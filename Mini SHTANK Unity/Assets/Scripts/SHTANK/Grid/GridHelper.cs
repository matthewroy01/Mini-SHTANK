using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace SHTANK.Grid
{
    public static class GridHelper
    {
        public static void SetEnemySpaceAndSurroundingTypes(GridSpaceObject enemySpace)
        {
            List<GridSpaceObject> visitedList = new();
            visitedList.Clear();

            // enemy space
            _SetGridSpaceType(GridSpaceType.EnemySpace,
                true,
                enemySpace);
            
            // battlefield
            GridConnections enemySpaceGridConnections = enemySpace.GridConnections;
            _SetGridSpaceType(GridSpaceType.Battlefield,
                true,
                enemySpaceGridConnections.Up.GridSpaceObject,
                enemySpaceGridConnections.Down.GridSpaceObject,
                enemySpaceGridConnections.Left.GridSpaceObject,
                enemySpaceGridConnections.Right.GridSpaceObject,
                enemySpaceGridConnections.UpLeft.GridSpaceObject,
                enemySpaceGridConnections.UpRight.GridSpaceObject,
                enemySpaceGridConnections.DownLeft.GridSpaceObject,
                enemySpaceGridConnections.DownRight.GridSpaceObject);

            // outskirts
            foreach (GridSpaceObject battlefieldGridSpaceObject in visitedList)
            {
                GridConnections battlefieldConnections = battlefieldGridSpaceObject.GridConnections;
                _SetGridSpaceType(GridSpaceType.Outskirts,
                    false,
                    battlefieldConnections.Up.GridSpaceObject,
                    battlefieldConnections.Down.GridSpaceObject,
                    battlefieldConnections.Left.GridSpaceObject,
                    battlefieldConnections.Right.GridSpaceObject,
                    battlefieldConnections.UpLeft.GridSpaceObject,
                    battlefieldConnections.UpRight.GridSpaceObject,
                    battlefieldConnections.DownLeft.GridSpaceObject,
                    battlefieldConnections.DownRight.GridSpaceObject);
            }
            
            enemySpace.GridConnections.Up.GridSpaceObject.SetGridSpaceType(GridSpaceType.Battlefield);

            void _SetGridSpaceType(GridSpaceType gridSpaceType, bool addToList, params GridSpaceObject[] gridSpaceObjectArray)
            {
                foreach (GridSpaceObject gridSpaceObject in gridSpaceObjectArray)
                {
                    if (visitedList.Contains(gridSpaceObject))
                        continue;
                    
                    gridSpaceObject.SetGridSpaceType(gridSpaceType);

                    if (!addToList)
                        continue;
                    
                    visitedList.Add(gridSpaceObject);
                }
            }
        }

        public static void ResetGridSpaceTypes(List<GridSpaceObject> gridSpaceObjectList)
        {
            foreach (GridSpaceObject gridSpaceObject in gridSpaceObjectList)
            {
                gridSpaceObject.SetGridSpaceType(GridSpaceType.None);
            }
        }
        
        public static void GenerateGrid(uint width, uint height, GridSpaceObject gridSpaceObjectPrefab, Transform gridSpaceObjectParent, List<GridSpaceObject> gridSpaceObjectList)
        {
            Vector3 spawnPosition;
            spawnPosition.y = 0.0f;

            GridSpaceObject[,] gridSpaceArray = new GridSpaceObject[width, height];

            int x, y;
            string id = "";
            
            _PopulateArray();
            _SetConnections();
            
            void _PopulateArray()
            {
                for (x = 0; x < gridSpaceArray.GetLength(0); ++x)
                {
                    for (y = 0; y < gridSpaceArray.GetLength(1); ++y)
                    {
                        id = "(" + x + ", " + y + ")";
                        
                        GridSpaceObject gridSpaceObject = _CreateGridSpaceObject();
                        spawnPosition.x = x;
                        spawnPosition.z = y;
                        gridSpaceObject.transform.position = spawnPosition;
                        gridSpaceObject.SetIntWorldPosition(VectorHelper.RoundVector3(spawnPosition));
                        gridSpaceObject.SetID(id);
                        gridSpaceObject.UpdateGameObjectName();

                        gridSpaceArray[x, y] = gridSpaceObject;
                        gridSpaceObjectList.Add(gridSpaceObject);
                    }
                }
            }
            
            void _SetConnections()
            {
                for (x = 0; x < gridSpaceArray.GetLength(0); ++x)
                {
                    for (y = 0; y < gridSpaceArray.GetLength(1); ++y)
                    {
                        GridSpaceObject up = TryGetObjectFrom2DArray(gridSpaceArray, x, y + 1);
                        GridSpaceObject down = TryGetObjectFrom2DArray(gridSpaceArray, x, y - 1);
                        GridSpaceObject left = TryGetObjectFrom2DArray(gridSpaceArray, x - 1, y);
                        GridSpaceObject right = TryGetObjectFrom2DArray(gridSpaceArray, x + 1, y);
                        GridSpaceObject upLeft = TryGetObjectFrom2DArray(gridSpaceArray, x - 1, y + 1);
                        GridSpaceObject upRight = TryGetObjectFrom2DArray(gridSpaceArray, x + 1, y + 1);
                        GridSpaceObject downLeft = TryGetObjectFrom2DArray(gridSpaceArray, x - 1, y - 1);
                        GridSpaceObject downRight = TryGetObjectFrom2DArray(gridSpaceArray, x + 1, y - 1);
                    
                        gridSpaceArray[x, y].SetGridConnections(new GridConnections(up, down, left, right, upLeft, upRight, downLeft, downRight));
                    }
                }
            }

            GridSpaceObject _CreateGridSpaceObject()
            {
                return Object.Instantiate(gridSpaceObjectPrefab, gridSpaceObjectParent);
            }
        }

        public static GridSpaceObject GetClosestGridSpace(Vector3 worldPosition, List<GridSpaceObject> gridSpaceObjectList)
        {
            Vector3Int intWorldPosition = VectorHelper.RoundVector3(worldPosition);
            intWorldPosition.y = 0;

            foreach (GridSpaceObject gridSpaceObject in gridSpaceObjectList)
            {
                if (gridSpaceObject.IntWorldPosition == intWorldPosition)
                {
                    return gridSpaceObject;
                }
            }
            
            Debug.LogWarning("GridContainer: Provided world position was not found on the grid. Are you off-grid?");
            return null;
        }
        
        private static T TryGetObjectFrom2DArray<T>(T[,] array, int x, int y)
        {
            if (x < 0 || y < 0 || x >= array.GetLength(0) || y >= array.GetLength(1))
                return default;

            return array[x, y];
        }
    }
}