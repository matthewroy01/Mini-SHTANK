using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

namespace SHTANK.Input
{
    public class InputManager : Singleton<InputManager>, SHTANKControls.IOverworldActions
    {
        public Vector2 Movement => _movement;
        public bool Jump => _jump;
        
        private SHTANKControls _shtankControls;
        private Vector2 _movement;
        private bool _jump;
        
        protected override void Awake()
        {
            base.Awake();
            
            _shtankControls = new SHTANKControls();
            _shtankControls.Overworld.SetCallbacks(this);
            _shtankControls.Overworld.Enable();
        }

        private void LateUpdate()
        {
            ResetBooleans();
        }

        private void ResetBooleans()
        {
            _jump = false;
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            _movement = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            _jump = InputHelper.CallbackContextTypeIsValid(context, CallbackContextType.Performed);
        }
    }
}