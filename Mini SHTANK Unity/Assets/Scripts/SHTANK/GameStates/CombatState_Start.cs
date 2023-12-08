using System;
using System.Collections;
using Utility.StateMachine;

namespace SHTANK.GameStates
{
    public class CombatState_Start : ManagerState<GameManager>
    {
        public event Action DoneWithAnimation;
        
        // TODO: create animation when combat starts
        
        public override void EnterState()
        {
            StartCoroutine(CombatStartRoutine());
        }

        public override void ExitState()
        {
            
        }

        public override void ProcessState()
        {
            
        }

        public override void ProcessStateFixed()
        {
            
        }

        private IEnumerator CombatStartRoutine()
        {
            // TODO: spawn player character at position according to saved Formation
            yield return null;

            DoneWithAnimation?.Invoke();
        }
    }
}