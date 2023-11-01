using System;
using SHTANK.Combat;
using SHTANK.Data.CombatEntities;
using UnityEngine;

namespace SHTANK.Overworld
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private CombatEntityDefinition _combatEntityDefinition;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMovement>() == null)
                return;
            
            CombatManager.Instance.BeginCombat(_combatEntityDefinition, transform.position);
        }
    }
}