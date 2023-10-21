using UnityEngine;

namespace SHTANK.Grid
{
    public class GridManager : MonoBehaviour
    {
        private GridContainer _gridContainer;

        private void Awake()
        {
            _gridContainer = new GridContainer(5, 5);
        }
    }
}