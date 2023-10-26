using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Utility.Testing
{
    public class CallFunctionOnInput : MonoBehaviour
    {
        [SerializeField] private UnityEvent _inputEvent1;
        [SerializeField] private UnityEvent _inputEvent2;

        private bool _inputPressed1;
        private bool _inputPressed2;

        private void Update()
        {
            PollInput();
            TryInvokeEvent();

            _inputPressed1 = _inputPressed2 = false;
        }

        private void PollInput()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
                _inputPressed1 = true;

            if (Keyboard.current.digit2Key.wasPressedThisFrame)
                _inputPressed2 = true;
        }

        private void TryInvokeEvent()
        {
            if (_inputPressed1 == true)
                _inputEvent1.Invoke();
            
            if (_inputPressed2 == true)
                _inputEvent2.Invoke();
        }
    }
}