using System;
using DG.Tweening;
using UnityEngine;

namespace Utility.DOTween.Transform
{
    [Serializable]
    public class LocalMoveYYoyoTweenHelper : TweenHelper
    {
        [SerializeField] private UnityEngine.Transform _target;
        [SerializeField] private float _endValue;
        [SerializeField] private float _duration;
        
        public override void DoTween(Action callback = null)
        {
            StopTween();
            
            Tween = _target.transform.DOLocalMoveY(_endValue, _duration).SetLoops(-1, LoopType.Yoyo).OnComplete( delegate
            {
                InvokeCallback(callback);
            }).SetEase(Ease.InOutQuad);;
        }
    }
}