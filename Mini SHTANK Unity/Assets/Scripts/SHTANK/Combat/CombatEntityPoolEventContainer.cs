using UnityEngine;
using Utility.Pooling;

namespace SHTANK.Combat
{
    public class CombatEntityPoolEventContainer : PoolEventContainer<CombatEntity>
    {
        public CombatEntityPoolEventContainer(CombatEntity prefab, Transform transform) : base(prefab, transform) { }

        public override CombatEntity Create()
        {
            return Object.Instantiate(Prefab, Transform);
        }

        public override void OnGet(CombatEntity toGet)
        {
            toGet.gameObject.SetActive(true);
        }

        public override void OnRelease(CombatEntity toFree)
        {
            toFree.gameObject.SetActive(false);
        }

        public override void OnDestroy(CombatEntity toDestroy)
        {
            Object.Destroy(toDestroy.gameObject);
        }
    }
}