using System;
using System.Collections.Generic;
using SHTANK.Data.Cards;
using SHTANK.Input;
using SHTANK.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using Utility;
using Utility.Pooling;

namespace SHTANK.Cards
{
    public class CardManager : Singleton<CardManager>
    {
        [Header("Prefabs")]
        [SerializeField] private CardObject _cardObjectPrefab;
        [SerializeField] private HandObject _handObjectPrefab;
        [SerializeField] private DeckObject _deckObjectPrefab;
        [SerializeField] private Transform _handsParent;
        [Header("References")]
        [SerializeField] private CardPreview _cardPreview;
        [Header("Testing")]
        [SerializeField] private List<CardDefinition> _testCards = new();
        private ObjectPool<CardObject> _cardObjectPool;
        private ObjectPool<HandObject> _handObjectPool;
        private ObjectPool<DeckObject> _deckObjectPool;
        private CardObjectPoolEventContainer _cardObjectPoolEventContainer;
        private HandObjectPoolEventContainer _handObjectPoolEventContainer;
        private DeckObjectPoolEventContainer _deckObjectPoolEventContainer;
        private readonly List<HandObject> _activeHands = new();
        private Vector2 _screenSpaceMousePosition;
        private CardObject _selectedCard;
        private readonly List<CardObject> _mousedOverCards = new();
        private int _highestSiblingIndex;
        private int _highestIndex;
        private int _siblingIndex;

        protected override void Awake()
        {
            base.Awake();
            
            _cardObjectPoolEventContainer = new CardObjectPoolEventContainer(_cardObjectPrefab, _handsParent);
            _cardObjectPool = PoolHelper<CardObject>.CreatePool(_cardObjectPoolEventContainer);

            _handObjectPoolEventContainer = new HandObjectPoolEventContainer(_handObjectPrefab, _handsParent);
            _handObjectPool = PoolHelper<HandObject>.CreatePool(_handObjectPoolEventContainer);

            _deckObjectPoolEventContainer = new DeckObjectPoolEventContainer(_deckObjectPrefab, _handsParent);
            _deckObjectPool = PoolHelper<DeckObject>.CreatePool(_deckObjectPoolEventContainer);
        }
        
        public void DrawCards()
        {
            const int numberOfHands = 3, numberOfCardsPerHand = 4;

            for (int i = 0; i < numberOfHands; ++i)
            {
                HandObject handObject = _handObjectPool.Get();

                for (int j = 0; j < numberOfCardsPerHand; ++j)
                {
                    // TODO: pull cards from a deck...
                    CardDefinition cardDefinition = RandomHelper.GetRandomItem(_testCards);
                    CardObject cardObject = _cardObjectPool.Get();
                    cardObject.Initialize(cardDefinition);
                    cardObject.ApplyOffset(j, numberOfCardsPerHand);
                    
                    handObject.TryAddCard(cardObject);
                }
                
                _activeHands.Add(handObject);
            }
        }

        public void InteractWithCards()
        {
            MouseOverCards();
            ClickCards();
        }

        // TODO: move this function and anything related to card "input" to a separate class
        private void MouseOverCards()
        {
            _mousedOverCards.Clear();

            _highestSiblingIndex = int.MinValue;
            _highestIndex = 0;
            _screenSpaceMousePosition = Mouse.current.position.ReadValue();
            
            _FindMousedOverCards();
            _FindHighestCard();
            _SelectCard();

            void _FindMousedOverCards()
            {
                foreach (HandObject handObject in _activeHands)
                {
                    foreach (CardObject cardObject in handObject.CardObjectList)
                    {
                        if (!RectTransformUtility.RectangleContainsScreenPoint(cardObject.ArtContainer, _screenSpaceMousePosition))
                            continue;

                        _mousedOverCards.Add(cardObject);
                    }
                }
            }

            void _FindHighestCard()
            {
                for (int i = 0; i < _mousedOverCards.Count; ++i)
                {
                    _siblingIndex = _mousedOverCards[i].transform.GetSiblingIndex();
                    if (_siblingIndex <= _highestSiblingIndex)
                        continue;
                    
                    _highestSiblingIndex = _siblingIndex;
                    _highestIndex = i;
                }
            }

            void _SelectCard()
            {
                if (_mousedOverCards.Count == 0)
                {
                    if (_selectedCard == null)
                        return;
                    
                    _selectedCard.Deselect();
                    _selectedCard = null;

                    return;
                }
                
                if (_selectedCard != null)
                {
                    if (_selectedCard == _mousedOverCards[_highestIndex])
                        return;
                    
                    _selectedCard.Deselect();
                    _selectedCard = _mousedOverCards[_highestIndex];
                    _selectedCard.Select();
                }
                else
                {
                    _selectedCard = _mousedOverCards[_highestIndex];
                    _selectedCard.Select();
                }
            }
        }

        private void ClickCards()
        {
            if (_selectedCard == null)
                return;

            if (!InputManager.Instance.Confirm)
                return;
            
            _cardPreview.AddCard(_selectedCard.CardDefinition);
        }
    }
}
