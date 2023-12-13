using System.Collections.Generic;
using SHTANK.Data.Cards;
using UnityEngine;
using UnityEngine.Pool;
using Utility.Pooling;

namespace SHTANK.UI
{
    public class CardPreview : MonoBehaviour
    {
        [SerializeField] private CardPreviewTextBox _cardPreviewTextBoxPrefab;
        [SerializeField] private Transform _cardPreviewTextBoxParent;
        [Header("Text")]
        [SerializeField] private string _firstMessage;
        [SerializeField] private string _subsequentMessage;
        
        private ObjectPool<CardPreviewTextBox> _cardPreviewTextBoxPool;
        private CardPreviewTextBoxPoolEventContainer _cardPreviewTextBoxPoolEventContainer;
        private readonly Stack<CardPreviewTextBox> _activeCardPreviewTextBoxStack = new();

        private void Awake()
        {
            _cardPreviewTextBoxPoolEventContainer = new CardPreviewTextBoxPoolEventContainer(_cardPreviewTextBoxPrefab, _cardPreviewTextBoxParent);
            _cardPreviewTextBoxPool = PoolHelper<CardPreviewTextBox>.CreatePool(_cardPreviewTextBoxPoolEventContainer);
        }

        public void AddCard(CardDefinition cardDefinition)
        {
            CardPreviewTextBox cardPreviewTextBox = _cardPreviewTextBoxPool.Get();
            cardPreviewTextBox.Initialize(_activeCardPreviewTextBoxStack.Count == 0 ? _firstMessage : _subsequentMessage, cardDefinition);
            
            _activeCardPreviewTextBoxStack.Push(cardPreviewTextBox);
        }

        public void RemoveCard()
        {
            if (_activeCardPreviewTextBoxStack.Count == 0)
                return;
            
            _cardPreviewTextBoxPool.Release(_activeCardPreviewTextBoxStack.Pop());
        }

        public void RemoveAllCards()
        {
            while (_activeCardPreviewTextBoxStack.Count > 0)
            {
                RemoveCard();
            }
        }
    }
}