using SHTANK.Grid;
using UnityEngine;

namespace SHTANK.Combat
{
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] private GridManager _gridManager;

        public void BeginCombat()
        {
            _gridManager.InitializeGridForCombat(new Vector3(5.0f, 0.0f, 5.0f));
        }

        public void EndCombat()
        {
            _gridManager.ClearGridAfterCombat();
        }
    }
}