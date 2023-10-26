using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Utility.Testing
{
    public class CallFunctionOnInput : MonoBehaviour
    {
        [SerializeField] private UnityEvent _inputEvent;

        private bool _inputPressed;

        private void Update()
        {
            PollInput();
            TryInvokeEvent();

            _inputPressed = false;
        }

        private void PollInput()
        {
            if (Keyboard.current.pKey.wasPressedThisFrame)
                _inputPressed = true;
        }

        private void TryInvokeEvent()
        {
            if (_inputPressed == true)
                _inputEvent.Invoke();
        }
    }
}