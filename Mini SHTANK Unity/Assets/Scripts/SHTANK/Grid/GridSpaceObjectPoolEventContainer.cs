using UnityEngine;
using Utility.Pooling;

namespace SHTANK.Grid
{
    public class GridSpaceObjectPoolEventContainer : PoolEventContainer<GridSpaceObject>
    {
        public GridSpaceObjectPoolEventContainer(GridSpaceObject prefab, Transform transform) : base(prefab, transform) { }

        public override GridSpaceObject Create()
        {
            GridSpaceObject tmp = Object.Instantiate(Prefab, Transform);
            return tmp;
        }

        public override void OnGet(GridSpaceObject toGet)
        {
            toGet.gameObject.SetActive(true);
        }

        public override void OnRelease(GridSpaceObject toFree)
        {
            toFree.gameObject.SetActive(false);
        }

        public override void OnDestroy(GridSpaceObject toDestroy)
        {
            Object.Destroy(toDestroy);
        }
    }
}