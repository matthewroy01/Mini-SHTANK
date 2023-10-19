using System;
using UnityEngine;

namespace SHTANK.Data.Cards
{
    [Serializable]
    public class CardEffect_Defend
    {
        [Range(0.0f, 100.0f)]
        [SerializeField] private float _damageReductionPercentage;
    }
}