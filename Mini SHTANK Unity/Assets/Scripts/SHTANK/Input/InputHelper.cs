using System;
using UnityEngine.InputSystem;

namespace SHTANK.Input
{
    public static class InputHelper
    {
        public static bool CallbackContextTypeIsValid(InputAction.CallbackContext callbackContext, CallbackContextType callbackContextType)
        {
            switch (callbackContextType)
            {
                case CallbackContextType.Started:
                    return callbackContext.started;
                case CallbackContextType.Performed:
                    return callbackContext.performed;
                case CallbackContextType.Canceled:
                    return callbackContext.canceled;
                case CallbackContextType.StartedOrPerformed:
                    return callbackContext.started || callbackContext.performed;
                case CallbackContextType.StartedOrCanceled:
                    return callbackContext.started || callbackContext.canceled;
                case CallbackContextType.PerformedOrCanceled:
                    return callbackContext.performed || callbackContext.canceled;
                default:
                    return false;
            }
        }
    }
}