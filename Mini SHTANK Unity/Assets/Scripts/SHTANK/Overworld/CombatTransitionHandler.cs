using System.Collections;
using DG.Tweening;
using SHTANK.Cameras;
using SHTANK.Data;
using TMPro;
using UnityEngine;

namespace SHTANK.Overworld
{
    public class CombatTransitionHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CameraManager _cameraManager;
        [Header("Visuals")]
        [SerializeField] private TextMeshProUGUI _vsTextTextMeshProUGUI;
        [SerializeField] private CanvasGroup _vsTextCanvasGroup;

        private Tweener _vsTextMeshProUGUIScaleTween;
        private Tweener _vsTextCanvasGroupFadeTween;
        private Tweener _mainCameraZoomTween;
        private Tweener _mainCameraRotationTween;
        private Tweener _splitScreenCameraZoomTween;
        private Tweener _splitScreenCameraRotationTween;

        public IEnumerator SeparateOverworldPlayerAndEnemy(float duration, Player player, Enemy enemy)
        {
            // TODO: check distance between overworld player and enemy, if they're too close, move the player away

            yield return new WaitForSeconds(duration);
        }

        public IEnumerator ZoomInCameras(float duration, Player player, Enemy enemy)
        {
            _cameraManager.ToggleSplitScreenCamera(true);


            Vector3 mainCameraTargetPosition = player.transform.position - (Vector3.forward * 0.3f) + (Vector3.up * 0.7f) + (Vector3.right * 0.3f);
            Vector3 splitScreenCameraTargetPosition = enemy.transform.position - (Vector3.forward * 0.3f) + (Vector3.up * 0.8f) + (Vector3.right * -0.2f);

            _mainCameraZoomTween?.Kill();
            _mainCameraRotationTween?.Kill();
            _splitScreenCameraZoomTween?.Kill();
            _splitScreenCameraRotationTween?.Kill();

            _mainCameraZoomTween = _cameraManager.MainCameraTransform.DOMove(mainCameraTargetPosition, duration).SetEase(Ease.InBack);
            _mainCameraRotationTween = _cameraManager.MainCameraTransform.DORotate(Vector3.forward, duration).SetEase(Ease.InOutQuad);
            _splitScreenCameraZoomTween = _cameraManager.SplitScreenCameraTransform.DOMove(splitScreenCameraTargetPosition, duration).SetEase(Ease.InBack);
            _splitScreenCameraRotationTween = _cameraManager.SplitScreenCameraTransform.DORotate(Vector3.forward, duration).SetEase(Ease.InOutQuad);

            yield return new WaitForSeconds(duration);
        }

        public void DoVSEffect()
        {
            _vsTextMeshProUGUIScaleTween?.Kill();
            _vsTextCanvasGroupFadeTween?.Kill();

            float duration = 1.0f;
            _vsTextTextMeshProUGUI.transform.localScale = Vector2.one * 3.0f;
            _vsTextMeshProUGUIScaleTween = _vsTextTextMeshProUGUI.transform.DOScale(Vector2.one, duration);
            _vsTextCanvasGroupFadeTween = _vsTextCanvasGroup.DOFade(1.0f, duration);

            // TODO: add other visuals here, such as particle systems
        }

        private void ClearVSEffect()
        {
            _vsTextMeshProUGUIScaleTween?.Kill();
            _vsTextCanvasGroupFadeTween?.Kill();

            float duration = 0.3f;
            _vsTextTextMeshProUGUI.transform.DOScale(Vector2.one * 3.0f, duration);
            _vsTextCanvasGroup.DOFade(0.0f, duration);
        }

        public IEnumerator ZoomOutCameras(float duration, CameraStateParameters cameraStateParameters)
        {
            ClearVSEffect();

            _mainCameraZoomTween?.Kill();
            _mainCameraRotationTween?.Kill();
            _splitScreenCameraZoomTween?.Kill();
            _splitScreenCameraRotationTween?.Kill();

            Vector3 targetPosition = _cameraManager.CombatTargetTransform.position + cameraStateParameters.PositionOffset;
            Vector3 targetRotation = new Vector3(cameraStateParameters.PitchAngle, 0.0f, 0.0f);

            _mainCameraZoomTween = _cameraManager.MainCameraTransform.DOMove(targetPosition, duration).SetEase(Ease.InOutQuint);
            _mainCameraRotationTween = _cameraManager.MainCameraTransform.DORotate(targetRotation, duration).SetEase(Ease.InOutQuint);
            _splitScreenCameraZoomTween = _cameraManager.SplitScreenCameraTransform.DOMove(targetPosition, duration).SetEase(Ease.InOutQuint);
            _splitScreenCameraRotationTween = _cameraManager.SplitScreenCameraTransform.DORotate(targetRotation, duration).SetEase(Ease.InOutQuint);

            yield return new WaitForSeconds(duration);

            _cameraManager.ToggleSplitScreenCamera(false);
        }
    }
}