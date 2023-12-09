using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using SHTANK.Data.Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace SHTANK.Cards
{
    public class CardObject : MonoBehaviour
    {
        public RectTransform ArtContainer => _artContainer;
        public CardDefinition CardDefinition => _cardDefinition;
        
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _image;
        [Header("Deck Visuals")]
        [SerializeField] private RectTransform _artContainer;
        [MinMaxSlider(-90.0f, 90.0f)]
        [SerializeField] private Vector2 _deckRotationMinMax;
        [SerializeField] private float _deckPositionAmplitude;
        [SerializeField] private float _selectedOffset;
        [Header("PreviewVisuals")]
        [MinMaxSlider(-90.0f, 90.0f)]
        [SerializeField] private Vector2 _previewRotationMinMax;
        private CardDefinition _cardDefinition;
        private List<CardEffectInfo> _cardEffectList = new();
        private float _yOffset;
        private Tween _selectionTween;

        public void Initialize(CardDefinition cardDefinition)
        {
            _cardDefinition = cardDefinition;
            
            _text.text = _cardDefinition.CardName;
            _image.sprite = _cardDefinition.CardIcon;
            
            _cardEffectList.Clear();
            _cardEffectList = _cardDefinition.CardEffects;
        }

        public void ApplyOffset(int cardID, int numberOfCards)
        {
            float t = numberOfCards <= 1 ? 0.5f : cardID / (numberOfCards - 1.0f);

            _yOffset = _GetPosition();

            _artContainer.eulerAngles = new Vector3(0.0f, 0.0f, _GetRotation());
            _artContainer.anchoredPosition = new Vector2(0.0f, _yOffset);

            float _GetRotation()
            {
                return Mathf.Lerp(_deckRotationMinMax.y, _deckRotationMinMax.x, t);
            }

            float _GetPosition()
            {
                float radian = Mathf.Lerp(0.0f, Mathf.PI, t);
                return Mathf.Sin(radian) * _deckPositionAmplitude;
            }
        }

        public void RandomlyRotate()
        {
            float randomZ = RandomHelper.GetRandomFloat(_previewRotationMinMax);
            _artContainer.eulerAngles = new Vector3(0.0f, 0.0f, randomZ);
        }

        public void Select()
        {
            _selectionTween?.Kill();
            _selectionTween = _artContainer.DOAnchorPosY(_yOffset + _selectedOffset, 0.25f);
        }

        public void Deselect()
        {
            _selectionTween?.Kill();
            _selectionTween = _artContainer.DOAnchorPosY(_yOffset, 0.25f);
        }
    }
}