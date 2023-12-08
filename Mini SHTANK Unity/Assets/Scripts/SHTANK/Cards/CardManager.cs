using System.Collections.Generic;
using SHTANK.Data.Cards;
using UnityEngine;
using UnityEngine.Pool;
using Utility;
using Utility.Pooling;

namespace SHTANK.Cards
{
    public class CardManager : Singleton<CardManager>
    {
        [SerializeField] private CardObject _cardObjectPrefab;
        [SerializeField] private HandObject _handObjectPrefab;
        [SerializeField] private DeckObject _deckObjectPrefab;
        [SerializeField] private Transform _handsParent;
        [Header("Testing")]
        [SerializeField] private List<CardDefinition> _testCards = new();
        private ObjectPool<CardObject> _cardObjectPool;
        private ObjectPool<HandObject> _handObjectPool;
        private ObjectPool<DeckObject> _deckObjectPool;
        private CardObjectPoolEventContainer _cardObjectPoolEventContainer;
        private HandObjectPoolEventContainer _handObjectPoolEventContainer;
        private DeckObjectPoolEventContainer _deckObjectPoolEventContainer;
        private List<HandObject> _activeHands = new();

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
    }
}
