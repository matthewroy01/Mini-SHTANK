using System;
using UnityEngine;

namespace SHTANK.Data.Cards
{
    [Serializable]
    public class CardEffect_Damage
    {
        public int Damage => _damage;
        public GridAreaFlags ValidSourceAreas => _validSourceAreas;
        
        [SerializeField] private int _damage;
        [SerializeField] private GridAreaFlags _validSourceAreas;
    }
}