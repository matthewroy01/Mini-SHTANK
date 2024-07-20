using SHTANK.Data;
using SHTANK.GameStates;
using SHTANK.Overworld;
using UnityEngine;
using Utility.StateMachine;

namespace SHTANK.Cameras
{
    public class CameraManager : MonoBehaviour
    {
        public Transform MainCameraTransform => _mainCameraTransform;
        public Transform SplitScreenCameraTransform => _splitScreenCameraTransform;
        public Transform CombatTargetTransform => _combatTargetTransform;
        public CameraStateParameters CombatCameraStateParameters => _combatCameraStateParameters;

        [SerializeField] private Transform _mainCameraTransform;
        [SerializeField] private Transform _splitScreenCameraTransform;
        [SerializeField] private Camera _splitScreenCamera;
        [SerializeField] private GameObject _quad;
        [Header("Targets")]
        [SerializeField] private Transform _overworldTargetTransform;
        [SerializeField] private Transform _combatTargetTransform;
        [Header("Camera Parameters")]
        [SerializeField] private CameraStateParameters _overworldCameraStateParameters;
        [SerializeField] private CameraStateParameters _combatCameraStateParameters;
        private Vector3 _targetPosition;
        private Vector3 _eulerAngles;
        private float _targetPitch;
        private EnemyManager _enemyManager;
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _enemyManager = EnemyManager.Instance;
            _playerMovement = FindObjectOfType<PlayerMovement>();
        }

        public void DoOverworldCameraBehavior()
        {
            MoveCamera(_mainCameraTransform, _overworldTargetTransform, _overworldCameraStateParameters);
            MoveCamera(_splitScreenCameraTransform, _overworldTargetTransform, _overworldCameraStateParameters);

            RotateCamera(_mainCameraTransform, _overworldTargetTransform, _overworldCameraStateParameters);
            RotateCamera(_splitScreenCameraTransform, _overworldTargetTransform, _overworldCameraStateParameters);
        }

        public void DoCombatCameraBehavior()
        {
            MoveCamera(_mainCameraTransform, _combatTargetTransform, _combatCameraStateParameters);
            MoveCamera(_splitScreenCameraTransform, _combatTargetTransform, _combatCameraStateParameters);

            RotateCamera(_mainCameraTransform, _combatTargetTransform, _combatCameraStateParameters);
            RotateCamera(_splitScreenCameraTransform, _combatTargetTransform, _combatCameraStateParameters);
        }

        public void DoZoomSplitCameraBehavior() { }

        private void MoveCamera(Transform cameraTransform, Transform targetTransform, CameraStateParameters cameraStateParameters)
        {
            if (cameraTransform == null || targetTransform == null || cameraStateParameters == null)
                return;

            _targetPosition = targetTransform.position + cameraStateParameters.PositionOffset;
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, _targetPosition, Time.deltaTime * cameraStateParameters.FollowSpeed);
        }

        private void RotateCamera(Transform cameraTransform, Transform targetTransform, CameraStateParameters cameraStateParameters)
        {
            _eulerAngles = cameraTransform.eulerAngles;
            _targetPitch = Mathf.Lerp(_eulerAngles.x, cameraStateParameters.PitchAngle, Time.deltaTime * cameraStateParameters.RotationSpeed);
            cameraTransform.eulerAngles = new Vector3(_targetPitch, _eulerAngles.y, _eulerAngles.z);
        }

        public void ToggleSplitScreenCamera(bool enable)
        {
            _splitScreenCamera.enabled = enable;
            _quad.SetActive(enable);
        }
    }
}