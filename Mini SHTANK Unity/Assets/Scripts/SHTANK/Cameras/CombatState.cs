namespace SHTANK.Cameras
{
    public class CombatState : CameraState
    {
        public override void EnterState() { }
        public override void ExitState() { }
        public override void ProcessState() { }

        public override void ProcessStateFixed()
        {
            Manager.MoveCamera(Manager.MainCameraTransform, _mainTargetTransform, _mainCameraStateParameters);
            Manager.MoveCamera(Manager.SplitScreenCameraTransform, _splitScreenTargetTransform, _useSeparateParameters ? _splitScreenCameraStateParameters : _mainCameraStateParameters);
        }
    }
}