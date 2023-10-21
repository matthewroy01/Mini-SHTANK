using UnityEngine;

namespace SHTANK.Grid
{
    public class GridSpace
    {
        public GridConnections GridConnections => _gridConnections;
        public GridSpaceType GridSpaceType => _gridSpaceType;
        
        private GridConnections _gridConnections;
        private readonly GridSpaceType _gridSpaceType = GridSpaceType.None;
        private string _id;

        public GridSpace(string id)
        {
            _id = id;
        }

        public void SetConnections(GridSpace up, GridSpace down, GridSpace left, GridSpace right,
            GridSpace upLeft, GridSpace upRight, GridSpace downLeft, GridSpace downRight)
        {
            _gridConnections = new GridConnections(up, down, left, right, upLeft, upRight, downLeft, downRight);
        }
    }
}
