using System.Collections.Generic;
using NaughtyAttributes;
using SHTANK.Data.Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SHTANK.Cards
{
    public class CardObject : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _image;
        [Header("Deck Visuals")]
        [SerializeField] private RectTransform _artContainer;
        [MinMaxSlider(-90.0f, 90.0f)]
        [SerializeField] private Vector2 _deckRotationMinMax;
        [SerializeField] private float _deckPositionAmplitude;
        private List<CardEffectInfo> _cardEffectList = new();

        public void Initialize(CardDefinition cardDefinition)
        {
            _text.text = cardDefinition.CardName;
            _image.sprite = cardDefinition.CardIcon;
            
            _cardEffectList.Clear();
            _cardEffectList = cardDefinition.CardEffects;
        }

        public void ApplyOffset(int cardID, int numberOfCards)
        {
            float t = numberOfCards <= 1 ? 0.5f : cardID / (numberOfCards - 1.0f);

            _artContainer.eulerAngles = new Vector3(0.0f, 0.0f, _GetRotation());
            _artContainer.anchoredPosition = new Vector2(0.0f, _GetPosition());

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
    }
}