using System;
using DG.Tweening;
using UnityEngine;

namespace Utility.DOTween.RectTransform
{
    [Serializable]
    public class LocalMoveYTweenHelper : TweenHelper
    {
        [SerializeField] private UnityEngine.RectTransform _target;
        [SerializeField] private float _endValue;
        [SerializeField] private float _duration;
    
        public override void DoTween(Action callback = null)
        {
            StopTween();
        
            Tween = _target.transform.DOLocalMoveY(_endValue, _duration).OnComplete( delegate
            {
                InvokeCallback(callback);
            }).SetEase(Ease.InOutQuad);;
        }
    }
}