using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace SHTANK.Data
{
    [CreateAssetMenu(fileName = "New Generic NPC Definition", menuName = "SHTANK/Generic NPC", order = 0)]
    public class GenericNPCDefinition : ScriptableObject
    {
        [Header("Sprites")]
        [SerializeField] private List<Sprite> _spriteHairList = new();
        [SerializeField] private List<Sprite> _spriteClothesDetailsList = new();
        [Header("Colors")]
        [SerializeField] private List<Color> _colorHairList = new();
        [SerializeField] private List<Color> _colorSkinToneList = new();
        [SerializeField] private List<Color> _colorClothesList = new();
        [SerializeField] private List<Color> _colorClothesDetailsList = new();
        [SerializeField] private List<Color> _colorShoesList = new();

        public Sprite GetRandomHairStyle()
        {
            return GetRandomItem(_spriteHairList);
        }

        public Sprite GetRandomClothesDetailsStyle()
        {
            return GetRandomItem(_spriteClothesDetailsList);
        }

        public Color GetRandomHairColor()
        {
            return GetRandomItem(_colorHairList);
        }

        public Color GetRandomSkinTone()
        {
            return GetRandomItem(_colorSkinToneList);
        }

        public Color GetRandomClothesColor()
        {
            return GetRandomItem(_colorClothesList);
        }

        public Color GetRandomClothesDetailsColor()
        {
            return GetRandomItem(_colorClothesDetailsList);
        }

        public Color GetRandomShoesList()
        {
            return GetRandomItem(_colorShoesList);
        }

        private T GetRandomItem<T>(List<T> list)
        {
            return RandomHelper.GetRandomItem(list);
        }
    }
}
