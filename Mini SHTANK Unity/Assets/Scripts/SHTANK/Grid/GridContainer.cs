﻿using UnityEngine;
using UnityEngine.Pool;

namespace SHTANK.Grid
{
    public class GridContainer
    {
        public GridSpace[,] GridSpaceArray => _gridSpaceArray;
        
        private readonly GridSpace[,] _gridSpaceArray;
        private Vector3 _spawnPosition;

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
                        _gridSpaceArray[x, y] = new GridSpace(id);
                        
                        _CreateAndPositionGridSpaceObject();
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
                        GridSpace right = GridHelper.TryGetObjectFrom2DArray(_gridSpaceArray, x + 1, y + 1);
                    
                        GridSpace upLeft = GridHelper.TryGetObjectFrom2DArray(_gridSpaceArray, x - 1, y + 1);
                        GridSpace upRight = GridHelper.TryGetObjectFrom2DArray(_gridSpaceArray, x + 1, y + 1);
                        GridSpace downLeft = GridHelper.TryGetObjectFrom2DArray(_gridSpaceArray, x - 1, y - 1);
                        GridSpace downRight = GridHelper.TryGetObjectFrom2DArray(_gridSpaceArray, x + 1, y - 1);
                    
                        _gridSpaceArray[x, y].SetConnections(up, down, left, right, upLeft, upRight, downLeft, downRight);
                    }
                }
            }

            void _CreateAndPositionGridSpaceObject()
            {
                _spawnPosition.x = x;
                _spawnPosition.z = y;
                
                GridSpaceObject tmp = objectPool.Get();
                tmp.transform.position = _spawnPosition;
                tmp.gameObject.name = "GridSpaceObject " + id;
            }
        }
    }
}