using System;
using NaughtyAttributes;
using SHTANK.Data;
using SHTANK.GameStates;
using UnityEngine;
using Utility.StateMachine;

namespace SHTANK.Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private GameManager _gameManager;
        [Header("States")]
        [SerializeField] private OverworldState _overworldState;
        [SerializeField] private CombatState _combatState;
        private StateMachine _stateMachine;
        private Vector3 _targetPosition;
        private Vector3 _eulerAngles;
        private float _targetPitch;

        private void OnEnable()
        {
            _gameManager.EnteringCombatState += OnEnteringCombatState;
            _gameManager.EnteringOverworldState += OnEnteringOverworldState;
        }

        private void OnDisable()
        {
            _gameManager.EnteringCombatState -= OnEnteringCombatState;
            _gameManager.EnteringOverworldState -= OnEnteringOverworldState;
        }

        private void OnEnteringCombatState()
        {
            _stateMachine.TryChangeState(_combatState);
        }

        private void OnEnteringOverworldState()
        {
            _stateMachine.TryChangeState(_overworldState);
        }

        private void Awake()
        {
            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {
            _overworldState.SetManager(this);
            _combatState.SetManager(this);
            
            _stateMachine = new StateMachine(_overworldState,
                new Connection(_overworldState, _combatState),
                new Connection(_combatState, _overworldState));
        }

        private void Update()
        {
            _stateMachine.CurrentState.ProcessState();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.ProcessStateFixed();
        }

        public void MoveCamera(Transform targetTransform, CameraStateParameters cameraStateParameters)
        {
            if (targetTransform == null)
                return;
            
            _targetPosition = targetTransform.position + cameraStateParameters.PositionOffset;
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _targetPosition, Time.deltaTime * cameraStateParameters.FollowSpeed);

            _eulerAngles = _cameraTransform.eulerAngles;
            _targetPitch = Mathf.Lerp(_eulerAngles.x, cameraStateParameters.PitchAngle, Time.deltaTime * cameraStateParameters.RotationSpeed);
            _cameraTransform.eulerAngles = new Vector3(_targetPitch, _eulerAngles.y, _eulerAngles.z);
        }
    }
}