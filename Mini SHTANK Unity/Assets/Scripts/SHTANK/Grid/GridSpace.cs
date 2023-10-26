using System;
using UnityEngine;

namespace SHTANK.Grid
{
    public class GridSpace
    {
        public GridConnections GridConnections => _gridConnections;
        public GridSpaceType GridSpaceType => _gridSpaceType;
        public Vector3Int IntWorldPosition => _intWorldPosition;
        
        private GridConnections _gridConnections;
        private GridSpaceType _gridSpaceType = GridSpaceType.None;
        private string _id;
        private GridSpaceObject _gridSpaceObject;
        private Vector3Int _intWorldPosition;

        public GridSpace(string id, GridSpaceObject gridSpaceObject)
        {
            _id = id;
            _gridSpaceObject = gridSpaceObject;

            Vector3 gridSpaceObjectPosition = _gridSpaceObject.transform.position;
            _intWorldPosition = new Vector3Int(Mathf.RoundToInt(gridSpaceObjectPosition.x), 0, Mathf.RoundToInt(gridSpaceObjectPosition.z));
        }

        public void SetConnections(GridSpace up, GridSpace down, GridSpace left, GridSpace right,
            GridSpace upLeft, GridSpace upRight, GridSpace downLeft, GridSpace downRight)
        {
            _gridConnections = new GridConnections(up, down, left, right, upLeft, upRight, downLeft, downRight);
        }

        public void SetType(GridSpaceType gridSpaceType)
        {
            if (gridSpaceType != _gridSpaceType)
                _gridSpaceObject.UpdateWithNewGridSpaceType(gridSpaceType);
            
            _gridSpaceType = gridSpaceType;
            _gridSpaceObject.gameObject.name = _id + (_gridSpaceType == GridSpaceType.None ? "" : Enum.GetName(typeof(GridSpaceType), _gridSpaceType));
        }
    }
}
