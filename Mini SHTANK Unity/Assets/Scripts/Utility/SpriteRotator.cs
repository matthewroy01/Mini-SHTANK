using DG.Tweening;
using UnityEngine;

namespace Utility
{
    public class SpriteRotator : MonoBehaviour
    {
        [SerializeField] private Transform _transformToRotate;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _velocityDeadZone;
        [SerializeField] private float _rotationDuration;

        private bool _facingRight = true;
        private float _speed;
        private Vector3 _velocity;
        private Vector2 _horizontalVelocity;
        private Tween _rotationTween;

        private void FixedUpdate()
        {
            ChangeDirections();
        }

        private void ChangeDirections()
        {
            _velocity = _rigidbody.velocity;
            _horizontalVelocity.x = _velocity.x;
            _horizontalVelocity.y = _velocity.z;

            if (_horizontalVelocity.magnitude < _velocityDeadZone)
                return;

            if (_facingRight)
            {
                if (!(_horizontalVelocity.x < 0.0f))
                    return;
                
                FaceLeft();
            }
            else
            {
                if (!(_horizontalVelocity.x > 0.0f))
                    return;

                FaceRight();
            }
        }

        private void FaceLeft()
        {
            _facingRight = false;
            
            _rotationTween.Kill();
            _rotationTween = _transformToRotate.DORotate(new Vector3(0, 180, 0), _rotationDuration).SetEase(Ease.OutSine);
        }

        private void FaceRight()
        {
            _facingRight = true;
                    
            _rotationTween.Kill();
            _rotationTween = _transformToRotate.DORotate(new Vector3(0, 0, 0), _rotationDuration).SetEase(Ease.OutSine);
        }
    }
}