using System;
using UnityEngine;
using NaughtyAttributes;

namespace SHTANK.Grid
{
    [Serializable]
    public class GridConnection
    {
        public GridSpaceObject GridSpaceObject => _gridSpaceObject;
        public float Weight => _weight;

        [ReadOnly] [SerializeField] private GridSpaceObject _gridSpaceObject;
        [ReadOnly] [SerializeField] private float _weight;

        public GridConnection(GridSpaceObject gridSpaceObjectObject, float weight)
        {
            _gridSpaceObject = gridSpaceObjectObject;
            _weight = weight;
        }
    }
}