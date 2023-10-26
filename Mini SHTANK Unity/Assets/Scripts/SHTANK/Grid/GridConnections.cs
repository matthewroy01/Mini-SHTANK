using System;
using JetBrains.Annotations;
using NaughtyAttributes;
using UnityEngine;

namespace SHTANK.Grid
{
    [Serializable]
    public class GridConnections
    {
        public GridConnection Up => _up;
        public GridConnection Down => _down;
        public GridConnection Left => _left;
        public GridConnection Right => _right;
        public GridConnection UpLeft => _upLeft;
        public GridConnection UpRight => _upRight;
        public GridConnection DownLeft => _downLeft;
        public GridConnection DownRight => _downRight;

        [ReadOnly] [SerializeField] private GridConnection _up;
        [ReadOnly] [SerializeField] private GridConnection _down;
        [ReadOnly] [SerializeField] private GridConnection _left;
        [ReadOnly] [SerializeField] private GridConnection _right;
        [ReadOnly] [SerializeField] private GridConnection _upLeft;
        [ReadOnly] [SerializeField] private GridConnection _upRight;
        [ReadOnly] [SerializeField] private GridConnection _downLeft;
        [ReadOnly] [SerializeField] private GridConnection _downRight;
        
        private const float CARDINAL_WEIGHT = 1.0f;
        private const float DIAGONAL_WEIGHT = 2.0f;

        public GridConnections(GridSpaceObject up, GridSpaceObject down, GridSpaceObject left, GridSpaceObject right,
            GridSpaceObject upLeft, GridSpaceObject upRight, GridSpaceObject downLeft, GridSpaceObject downRight)
        {
            _up = CreateGridConnection(up, false);
            _down = CreateGridConnection(down, false);
            _left = CreateGridConnection(left, false);
            _right = CreateGridConnection(right, false);
            _upLeft = CreateGridConnection(upLeft, true);
            _upRight = CreateGridConnection(upRight, true);
            _downLeft = CreateGridConnection(downLeft, true);
            _downRight = CreateGridConnection(downRight, true);
        }

        private GridConnection CreateGridConnection(GridSpaceObject gridSpaceObject, bool diagonal)
        {
            return gridSpaceObject == null ? null : new GridConnection(gridSpaceObject, diagonal ? DIAGONAL_WEIGHT : CARDINAL_WEIGHT);
        }
    }
}