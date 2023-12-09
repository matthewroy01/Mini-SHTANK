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
        public bool Confirm => _confirm;
        public bool Cancel => _cancel;
        
        private SHTANKControls _shtankControls;
        private Vector2 _movement;
        private bool _jump;
        private bool _confirm;
        private bool _cancel;
        
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
            _confirm = false;
            _cancel = false;
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            _movement = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            _jump = InputHelper.CallbackContextTypeIsValid(context, CallbackContextType.Performed);
        }

        public void OnConfirm(InputAction.CallbackContext context)
        {
            _confirm = InputHelper.CallbackContextTypeIsValid(context, CallbackContextType.Canceled);
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            _cancel = InputHelper.CallbackContextTypeIsValid(context, CallbackContextType.Canceled);
        }
    }
}