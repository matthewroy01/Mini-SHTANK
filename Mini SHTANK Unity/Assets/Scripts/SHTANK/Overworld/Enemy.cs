using System;
using SHTANK.Combat;
using UnityEngine;

namespace SHTANK.Overworld
{
    public class Enemy : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMovement>() == null)
                return;
            
            CombatManager.Instance.BeginCombat(transform.position);
        }
    }
}