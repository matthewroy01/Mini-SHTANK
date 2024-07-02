using UnityEngine;

namespace SHTANK.Grid
{
    public class GridSpaceVisual : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private Material _material;

        private static readonly int _proximityAlphaBaseColor = Shader.PropertyToID("_BaseColor");

        private void Awake()
        {
            _material = _meshRenderer.material;
        }

        public void SetColor(Color color)
        {
            _material.SetColor(_proximityAlphaBaseColor, color);
        }
    }
}