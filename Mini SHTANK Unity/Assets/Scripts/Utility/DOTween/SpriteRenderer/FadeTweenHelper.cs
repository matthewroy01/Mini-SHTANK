using System;
using DG.Tweening;
using UnityEngine;

namespace Utility.DOTween.SpriteRenderer
{
    [Serializable]
    public class FadeTweenHelper : TweenHelper
    {
        [SerializeField] private UnityEngine.SpriteRenderer _spriteRenderer;
        [SerializeField] [Range(0.0f, 1.0f)] private float _endValue;
        [SerializeField] private float _duration;
        
        public override void DoTween(Action callback = null)
        {
            StopTween();
            Tween = _spriteRenderer.DOFade(_endValue, _duration).OnComplete( delegate
            {
                InvokeCallback(callback);
            });
        }
    }
}