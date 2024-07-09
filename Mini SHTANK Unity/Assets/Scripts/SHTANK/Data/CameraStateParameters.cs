using NaughtyAttributes;
using UnityEngine;

namespace SHTANK.Data
{
    [CreateAssetMenu(fileName = "New Camera State Parameters", menuName = "SHTANK/Camera State Parameters", order = 1)]
    public class CameraStateParameters : ScriptableObject
    {
        public Vector3 PositionOffset => _positionOffset;
        public float FollowSpeed => _followSpeed;
        public float PitchAngle => _pitchAngle;
        public float RotationSpeed => _rotationSpeed;

        [Header("Position")]
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private float _followSpeed;
        [Header("Rotation")]
        [SerializeField] private float _pitchAngle;
        [SerializeField] private float _rotationSpeed;
    }
}