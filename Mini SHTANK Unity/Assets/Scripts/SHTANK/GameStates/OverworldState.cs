using SHTANK.Overworld;
using UnityEngine;
using Utility.StateMachine;

namespace SHTANK.GameStates
{
    public class OverworldState : ManagerState<GameManager>
    {
        [SerializeField] private PlayerMovement _playerMovement;
        
        public override void EnterState()
        {
            
        }

        public override void ExitState()
        {
            
        }

        public override void ProcessState()
        {
            _playerMovement.MyUpdate();
        }

        public override void ProcessStateFixed()
        {
            _playerMovement.MyFixedUpdate();
        }
    }
}