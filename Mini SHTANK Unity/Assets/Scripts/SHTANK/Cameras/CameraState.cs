using NaughtyAttributes;
using SHTANK.Data;
using UnityEngine;
using Utility.StateMachine;

namespace SHTANK.Cameras
{
    public abstract class CameraState : ManagerState<CameraManager>
    {
        [SerializeField] protected Transform _mainTargetTransform;
        [SerializeField] protected Transform _splitScreenTargetTransform;
        [SerializeField] protected bool _useSeparateParameters;
        [SerializeField] protected CameraStateParameters _mainCameraStateParameters;
        [SerializeField] [ShowIf("_useSeparateParameters")] protected CameraStateParameters _splitScreenCameraStateParameters;

        public abstract override void EnterState();
        public abstract override void ProcessState();
        public abstract override void ProcessStateFixed();
        public abstract override void ExitState();
    }
}