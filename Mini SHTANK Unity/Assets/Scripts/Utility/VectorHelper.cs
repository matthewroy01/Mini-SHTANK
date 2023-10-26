using UnityEngine;

namespace Utility
{
    public static class VectorHelper
    {
        public static Vector3Int RoundVector3(Vector3 vector)
        {
            return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
        }
    }
}