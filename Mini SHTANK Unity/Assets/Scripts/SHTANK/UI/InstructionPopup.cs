using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace SHTANK.UI
{
    public class InstructionPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _inDuration;
        [SerializeField] private float _outDuration;
        [SerializeField] private float _inTargetY;
        [SerializeField] private float _outTargetY;
        private bool _active;
        private Coroutine _slideCoroutine;
        private Tween _fadeTween;
        private Tween _slideTween;
        
        public void TrySlide(string text)
        {
            if (_slideCoroutine != null)
                StopCoroutine(_slideCoroutine);

            _slideCoroutine = StartCoroutine(TrySlideRoutine(text));
        }

        private IEnumerator TrySlideRoutine(string text)
        {
            if (_active && text == "")
            {
                yield return SlideOut(true);
            }
            else if (_active)
            {
                yield return SlideOut(false);
                _text.text = text;
                yield return SlideIn();
            }
            else
            {
                _text.text = text;
                yield return SlideIn();
            }
        }

        private IEnumerator SlideOut(bool setInactive)
        {
            yield return Slide(0.0f, _outTargetY, _outDuration);

            if (setInactive)
                _active = false;
        }

        private IEnumerator SlideIn()
        {
            _active = true;
            
            yield return Slide(1.0f, _inTargetY, _inDuration);
        }

        private IEnumerator Slide(float fadeEndValue, float anchorPosYEndValue, float duration)
        {
            _fadeTween?.Kill();
            _fadeTween = _canvasGroup.DOFade(fadeEndValue, duration).SetEase(Ease.InOutQuad);
            
            _slideTween?.Kill();
            _slideTween = _rectTransform.DOAnchorPosY(anchorPosYEndValue, duration).SetEase(Ease.InOutQuad);

            yield return new WaitForSeconds(duration);
        }
    }
}
