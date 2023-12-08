using System.Collections.Generic;
using UnityEngine;

namespace SHTANK.Data.Cards
{
    [CreateAssetMenu(fileName = "New Deck Definition", menuName = "SHTANK/Deck", order = 0)]
    public class DeckDefinition : ScriptableObject
    {
        public string DeckName => _deckName;
        public List<DeckCardInfo> CardList => _cardList;

        [SerializeField] private string _deckName;
        [Header("Cards")]
        [SerializeField] private List<DeckCardInfo> _cardList = new();
    }
}