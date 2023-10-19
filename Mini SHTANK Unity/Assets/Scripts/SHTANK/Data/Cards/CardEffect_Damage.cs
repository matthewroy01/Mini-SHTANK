using System;
using UnityEngine;

namespace SHTANK.Data.Cards
{
    [Serializable]
    public class CardEffect_Damage
    {
        [SerializeField] private int _damage;
        [SerializeField] private GridAreaFlags _validSourceAreas;
    }
}