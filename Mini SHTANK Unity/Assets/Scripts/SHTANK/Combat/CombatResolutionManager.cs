using System;
using System.Collections;
using UnityEngine;
using Utility;
using Utility.DOTween.CanvasGroup;

namespace SHTANK.Combat
{
    public class CombatResolutionManager : Singleton<CombatResolutionManager>
    {
        [SerializeField] private CanvasGroup _victoryCanvasGroup;
        [SerializeField] private CanvasGroup _defeatCanvasGroup;
        [SerializeField] private FadeTweenHelper _victoryInFadeTweenHelper;
        [SerializeField] private FadeTweenHelper _victoryOutFadeTweenHelper;
        [SerializeField] private FadeTweenHelper _defeatInFadeTweenHelper;
        [SerializeField] private FadeTweenHelper _defeatOutFadeTweenHelper;

        private CombatResolutionInfo _combatResolutionInfo;
        private Coroutine _combatResolutionCoroutine;

        public void StartCombatResolution(CombatResolutionInfo combatResolutionInfo)
        {
            _combatResolutionInfo = combatResolutionInfo;

            if (_combatResolutionCoroutine != null)
                StopCoroutine(_combatResolutionCoroutine);

            StartCoroutine(_combatResolutionInfo.PlayerVictory ? StartCombatVictoryCoroutine() : StartCombatDefeatCoroutine());
        }

        private IEnumerator StartCombatVictoryCoroutine()
        {
            // TODO: add to these fade in and out coroutines so that they do more animations for end-of-combat information (such as experience gain)
            yield return _FadeInVictoryScreen();

            IEnumerator _FadeInVictoryScreen()
            {
                _victoryInFadeTweenHelper.DoTween(() => SetCanvasGroupInteractable(_victoryCanvasGroup, true));

                yield return null;
            }
        }

        private IEnumerator StartCombatDefeatCoroutine()
        {
            yield return _FadeInDefeatScreen();

            IEnumerator _FadeInDefeatScreen()
            {
                _victoryInFadeTweenHelper.DoTween(() => SetCanvasGroupInteractable(_defeatCanvasGroup, true));

                yield return null;
            }
        }

        public void EndCombat(Action callback)
        {
            if (_combatResolutionCoroutine != null)
                StopCoroutine(_combatResolutionCoroutine);

            StartCoroutine(_combatResolutionInfo.PlayerVictory ? EndCombatVictoryCoroutine(callback) : EndCombatDefeatCoroutine(callback));
        }

        private IEnumerator EndCombatVictoryCoroutine(Action callback)
        {
            // TODO: add to these fade in and out coroutines so that they do more animations for end-of-combat information (such as experience gain)
            yield return _FadeOutVictoryScreen();

            IEnumerator _FadeOutVictoryScreen()
            {
                SetCanvasGroupInteractable(_victoryCanvasGroup, false);
                _victoryOutFadeTweenHelper.DoTween();

                yield return null;

                callback?.Invoke();
            }
        }

        private IEnumerator EndCombatDefeatCoroutine(Action callback)
        {
            yield return _FadeOutDefeatScreen();

            IEnumerator _FadeOutDefeatScreen()
            {
                SetCanvasGroupInteractable(_victoryCanvasGroup, false);
                _victoryOutFadeTweenHelper.DoTween();

                yield return null;

                callback?.Invoke();
            }
        }

        private void SetCanvasGroupInteractable(CanvasGroup canvasGroup, bool interactable)
        {
            canvasGroup.interactable = interactable;
            canvasGroup.blocksRaycasts = interactable;
        }
    }
}