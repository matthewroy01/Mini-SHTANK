using SHTANK.Overworld;
using UnityEngine;

namespace Utility
{
    public class TrackTransform : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private Transform _t;
        private static readonly int _playerPosition = Shader.PropertyToID("_PlayerPosition");
        private static readonly int _myPosition = Shader.PropertyToID("_MyPosition");

        private void Awake()
        {
            _t = FindObjectOfType<PlayerMovement>().transform;
            _meshRenderer.material.SetVector(_myPosition, transform.position);
        }

        private void Update()
        {
            _meshRenderer.material.SetVector(_playerPosition, _t.position);
        }
    }
}