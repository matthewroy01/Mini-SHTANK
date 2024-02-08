using NaughtyAttributes;
using UnityEngine;

namespace Utility
{
    public class ShaderPropertyTest : MonoBehaviour
    {
        [SerializeField] private string _propertyName;
        [SerializeField] private float _value;
        [SerializeField] private Renderer _renderer;
    
        [Button("Set Property")]
        public void SetProperty()
        {
            _renderer.material.SetFloat(_propertyName, _value);
        }
    }
}