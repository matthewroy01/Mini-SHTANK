using System;
using NaughtyAttributes;
using UnityEngine;

namespace Utility.Physics
{
    public class GroundChecker : MonoBehaviour
    {
        public event Action BecameGrounded;
        public event Action BecameAirborne;
        
        public bool Grounded => _grounded;

        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Transform _checkTransform;
        [SerializeField] private float _checkDistance;
        [SerializeField] [ReadOnly] private bool _grounded = false;
        private readonly RaycastHit[] _raycastHitArray = new RaycastHit[10];
        private int _hits;

        private void Update()
        {
            CheckGround();
        }

        private void CheckGround()
        {
            _hits = UnityEngine.Physics.RaycastNonAlloc(_checkTransform.position, Vector3.down, _raycastHitArray, _checkDistance, _layerMask);

            if (_hits > 0)
            {   
                UpdateGroundDetection(true, BecameGrounded);
                return;
            }
            
            UpdateGroundDetection(false, BecameAirborne);
        }

        private void UpdateGroundDetection(bool detected, Action toInvoke)
        {
            if (_grounded != detected)
                toInvoke?.Invoke();

            _grounded = detected;
        }
    }
}