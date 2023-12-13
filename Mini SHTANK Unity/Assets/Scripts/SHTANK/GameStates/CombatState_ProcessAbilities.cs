using System;
using UnityEngine;
using Utility.StateMachine;

namespace SHTANK.GameStates
{
    public class CombatState_ProcessAbilities : ManagerState<GameManager>
    {
        public event Action DoneProcessingAbilities;
        
        [SerializeField] private string _instructionText;
        
        private float _timer;
        
        public override void EnterState()
        {
            _timer = 0.0f;
            Manager.UpdateInstructionPopup(_instructionText);
        }
        
        public override void ExitState()
        {
            Manager.UpdateInstructionPopup();
        }
        
        public override void ProcessState()
        {
            _timer += Time.deltaTime;
            
            if (_timer > 1.5f)
                DoneProcessingAbilities?.Invoke();
        }
        
        public override void ProcessStateFixed() { }
    }
}
