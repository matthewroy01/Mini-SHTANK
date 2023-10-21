namespace SHTANK.Grid
{
    public class GridConnection
    {
        public GridSpace GridSpace => _gridSpace;
        public float Weight => _weight;

        private GridSpace _gridSpace;
        private float _weight;

        public GridConnection(GridSpace gridSpace, float weight)
        {
            _gridSpace = gridSpace;
            _weight = weight;
        }
    }
}