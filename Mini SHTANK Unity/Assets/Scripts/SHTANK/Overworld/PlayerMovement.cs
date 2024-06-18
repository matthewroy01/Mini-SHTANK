using System;
using SHTANK.Input;
using UnityEngine;
using Utility.Physics;

namespace SHTANK.Overworld
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GroundChecker _groundChecker;
        [Header("Movement")]
        [SerializeField] private float _acceleration = 1.0f;
        [SerializeField] private float _accelerationTarget = 1.0f;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _airborneAccelerationMultiplier = 1.0f;
        [SerializeField] private float _decceleration = 1.0f;
        [SerializeField] private float _maxSpeed = 1.0f;
        [Header("Jumping")]
        [SerializeField] private float _jumpForce = 1.0f;

        private InputManager _inputManager;
        private Vector2 _movementVector;
        private Vector3 _movementForce;
        private Vector3 _velocity;

        private void Awake()
        {
            _inputManager = InputManager.Instance;
        }

        public void MyUpdate()
        {
            Jumping();
        }

        public void MyFixedUpdate()
        {
            Movement();
            TerminalVelocity();
        }

        private void Movement()
        {
            _movementVector = _inputManager.Movement;

            if (_movementVector == Vector2.zero)
            {
                _movementForce = Vector3.Lerp(_movementForce, Vector3.zero, Time.deltaTime * _decceleration);
            }
            else
            {
                _movementForce = Vector3.Lerp(_movementForce, new Vector3(_movementVector.x * _accelerationTarget, 0.0f, _movementVector.y * _accelerationTarget), Time.deltaTime * DetermineAcceleration());   
            }

            _rigidbody.AddForce(_movementForce);
        }

        private void TerminalVelocity()
        {
            _velocity = _rigidbody.velocity;
            _velocity.y = 0.0f;

            if (_velocity.magnitude < _maxSpeed)
                return;

            _velocity = _velocity.normalized * _maxSpeed;
            _rigidbody.velocity = new Vector3(_velocity.x, _rigidbody.velocity.y, _velocity.z);
        }

        private void Jumping()
        {
            if (!_inputManager.Jump)
                return;
            
            if (!_groundChecker.Grounded)
                return;

            _velocity = _rigidbody.velocity;
            _rigidbody.velocity = new Vector3(_velocity.x, 0.0f, _velocity.z);
            _rigidbody.AddForce(Vector3.up * _jumpForce);
        }

        private float DetermineAcceleration()
        {
            return _groundChecker.Grounded ? _acceleration : _acceleration * _airborneAccelerationMultiplier;
        }

        public void StopVelocity()
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
