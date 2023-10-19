using System;
using UnityEngine;

namespace SHTANK.Data.Cards
{
    [Serializable]
    public class CardEffect_ChangeArea
    {
        [SerializeField] private GridAreaType _destinationArea = GridAreaType.None;
    }
}