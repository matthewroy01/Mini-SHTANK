﻿namespace SHTANK.Camera
{
    public class OverworldState : CameraState
    {
        public override void EnterState() { }
        public override void ExitState() { }
        public override void ProcessState() { }

        public override void ProcessStateFixed()
        {
            Manager.MoveCamera(_targetTransform, _cameraStateParameters);
        }
    }
}