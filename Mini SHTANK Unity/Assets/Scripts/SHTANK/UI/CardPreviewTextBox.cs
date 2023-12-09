using SHTANK.Cards;
using SHTANK.Data.Cards;
using TMPro;
using UnityEngine;

namespace SHTANK.UI
{
    public class CardPreviewTextBox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private CardObject _cardObject;

        public void Initialize(string textBoxText, CardDefinition _cardDefinition)
        {
            _text.text = textBoxText;
            _cardObject.Initialize(_cardDefinition);
            _cardObject.RandomlyRotate();
        }
    }
}
