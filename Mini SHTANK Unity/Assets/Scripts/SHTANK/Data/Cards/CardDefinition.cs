using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace SHTANK.Data.Cards
{
    [CreateAssetMenu(fileName = "New Card Definition", menuName = "SHTANK/Card", order = 0)]
    public class CardDefinition : ScriptableObject
    {
        public string CardName => _cardName;
        public string CardDescription => _cardDescription;
        public Sprite CardIcon => _cardIcon;
        public List<CardEffectInfo> CardEffects => _cardEffects;
        
        [Header("Info")]
        [SerializeField] private string _cardName = "Default Card Name";
        [TextArea(3, 10)]
        [SerializeField] private string _cardDescription = "Default Card Description";
        [ShowAssetPreview]
        [SerializeField] private Sprite _cardIcon;
        [Space]
        [Header("Effects")]
        [SerializeField] private List<CardEffectInfo> _cardEffects = new();
    }
}