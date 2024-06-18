using System;
using DG.Tweening;
using UnityEngine;

namespace Utility.DOTween.CanvasGroup
{
    [Serializable]
    public class FadeTweenHelper : TweenHelper
    {
        [SerializeField] private UnityEngine.CanvasGroup _canvasGroup;
        [SerializeField] [Range(0.0f, 1.0f)] private float _endValue;
        [SerializeField] private float _duration;
        
        public override void DoTween(Action callback = null)
        {
            StopTween();
            Tween = _canvasGroup.DOFade(_endValue, _duration).OnComplete( delegate
            {
                InvokeCallback(callback);
            });
        }
    }
}