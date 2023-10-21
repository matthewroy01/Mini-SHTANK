using JetBrains.Annotations;

namespace SHTANK.Grid
{
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

        private readonly GridConnection _up;
        private readonly GridConnection _down;
        private readonly GridConnection _left;
        private readonly GridConnection _right;
        private readonly GridConnection _upLeft;
        private readonly GridConnection _upRight;
        private readonly GridConnection _downLeft;
        private readonly GridConnection _downRight;
        
        private const float CARDINAL_WEIGHT = 1.0f;
        private const float DIAGONAL_WEIGHT = 2.0f;

        public GridConnections(GridSpace up, GridSpace down, GridSpace left, GridSpace right,
            GridSpace upLeft, GridSpace upRight, GridSpace downLeft, GridSpace downRight)
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

        private GridConnection CreateGridConnection(GridSpace gridSpace, bool diagonal)
        {
            return gridSpace == null ? null : new GridConnection(gridSpace, diagonal ? DIAGONAL_WEIGHT : CARDINAL_WEIGHT);
        }
    }
}