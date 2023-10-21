namespace SHTANK.Grid
{
    public static class GridHelper
    {
        public static T TryGetObjectFrom2DArray<T>(T[,] array, int x, int y)
        {
            if (x < 0 || y < 0 || x >= array.GetLength(0) || y >= array.GetLength(1))
                return default;

            return array[x, y];
        }
    }
}