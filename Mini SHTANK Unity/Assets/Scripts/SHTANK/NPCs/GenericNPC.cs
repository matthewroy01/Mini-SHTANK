using NaughtyAttributes;
using SHTANK.Data;
using UnityEngine;

namespace SHTANK.NPCs
{
    public class GenericNPC : MonoBehaviour
    {
        [SerializeField] private GenericNPCDefinition _genericNPCDefinition;
        [Header("Sprite Renderers")]
        [SerializeField] private SpriteRenderer _spriteRendererHair;
        [SerializeField] private SpriteRenderer _spriteRendererSkin;
        [SerializeField] private SpriteRenderer _spriteRendererClothes;
        [SerializeField] private SpriteRenderer _spriteRendererClothesDetails;
        [SerializeField] private SpriteRenderer _spriteRendererShoes;

        [Button("Test Randomization")]
        [ContextMenu("Test Randomization")]
        public void RandomizeAppearance()
        {
            if (_genericNPCDefinition == null)
                return;

            _spriteRendererHair.sprite = _genericNPCDefinition.GetRandomHairStyle();
            _spriteRendererHair.color = _genericNPCDefinition.GetRandomHairColor();

            _spriteRendererSkin.color = _genericNPCDefinition.GetRandomSkinTone();

            _spriteRendererClothes.color = _genericNPCDefinition.GetRandomClothesColor();

            _spriteRendererClothesDetails.sprite = _genericNPCDefinition.GetRandomClothesDetailsStyle();
            _spriteRendererClothesDetails.color = _genericNPCDefinition.GetRandomClothesDetailsColor();

            _spriteRendererShoes.color = _genericNPCDefinition.GetRandomShoesList();
        }

        [Button("Clear Randomization")]
        [ContextMenu("Clear Randomization")]
        private void ClearAppearance()
        {
            _spriteRendererHair.sprite = null;
            _spriteRendererHair.color = Color.white;

            _spriteRendererSkin.color = Color.white;

            _spriteRendererClothes.color = Color.white;

            _spriteRendererClothesDetails.sprite = null;
            _spriteRendererClothesDetails.color = Color.white;

            _spriteRendererShoes.color = Color.white;
        }
    }
}
