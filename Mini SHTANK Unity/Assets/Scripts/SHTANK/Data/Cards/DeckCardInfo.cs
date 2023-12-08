using System;
using UnityEngine;

namespace SHTANK.Data.Cards
{
    [Serializable]
    public class DeckCardInfo
    {
        public CardDefinition CardDefinition => _cardDefinition;
        public uint Amount => _amount;
        
        [SerializeField] private CardDefinition _cardDefinition;
        [SerializeField] private uint _amount = 1;
    }
}