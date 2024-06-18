using System;
using DG.Tweening;

namespace Utility.DOTween
{
    public abstract class TweenHelper
    {
        protected Tween Tween;
        
        public abstract void DoTween(Action callback = null);

        public virtual void StopTween()
        {
            Tween?.Kill();
        }

        protected void InvokeCallback(Action callback)
        {
            callback?.Invoke();
        }
    }
}