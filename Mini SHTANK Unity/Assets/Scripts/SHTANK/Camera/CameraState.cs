using SHTANK.Data;
using UnityEngine;
using Utility.StateMachine;

namespace SHTANK.Camera
{
    public abstract class CameraState : ManagerState<CameraManager>
    {
        [SerializeField] protected Transform _targetTransform;
        [SerializeField] protected CameraStateParameters _cameraStateParameters;
        
        public abstract override void EnterState();
        public abstract override void ProcessState();
        public abstract override void ProcessStateFixed();
        public abstract override void ExitState();
    }
}