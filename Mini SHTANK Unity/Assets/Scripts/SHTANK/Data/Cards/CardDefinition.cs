using System.Collections.Generic;
using UnityEngine;

namespace SHTANK.Data.Cards
{
    [CreateAssetMenu(fileName = "New Card Definition", menuName = "SHTANK/Card", order = 0)]
    public class CardDefinition : ScriptableObject
    {
        [Header("Info")]
        [SerializeField] private string _cardName = "Default Card Name";
        [TextArea(3, 10)]
        [SerializeField] private string _cardDescription = "Default Card Description";
        [Space]
        [Header("Effects")]
        [SerializeField] private List<CardEffectInfo> _cardEffects = new();
    }
}