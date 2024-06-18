using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Utility.DOTween.Transform
{
    [Serializable]
    public class LocalJumpWithAnimationCurveTweenHelper : TweenHelper
    {
        public float Duration => _duration;
        
        [SerializeField] private UnityEngine.Transform _target;
        [SerializeField] private Vector3 _endValue;
        [SerializeField] private float _jumpPower;
        [SerializeField] private int _numJumps;
        [SerializeField] private float _duration;
        [SerializeField] private AnimationCurve _ease;
        
        public override void DoTween(Action callback = null)
        {
            StopTween();
            Tween = _target.DOLocalJump(_endValue, _jumpPower, _numJumps, _duration).OnComplete( delegate
            {
                InvokeCallback(callback);
            }).SetEase(_ease);
        }

        public override void StopTween()
        {
            base.StopTween();

            _target.transform.localPosition = Vector3.zero;
        }
    }
}