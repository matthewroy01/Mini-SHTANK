using UnityEngine;

namespace Utility.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        public string StateName => _stateName;

        private string _stateName;

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void ProcessState();
        public abstract void ProcessStateFixed();

        public void SetStateName(string stateName)
        {
            _stateName = stateName;
        }
    }
}