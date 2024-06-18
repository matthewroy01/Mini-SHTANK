using System;
using SHTANK.Overworld;
using UnityEngine;
using Utility.StateMachine;

namespace SHTANK.GameStates
{
    public class OverworldState : ManagerState<GameManager>
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private EnemyManager _enemyManager;

        public override void EnterState()
        {
            _playerMovement.gameObject.SetActive(true);
        }

        public override void ExitState()
        {
            _playerMovement.gameObject.SetActive(false);
            _playerMovement.StopVelocity();
            _enemyManager.ToggleEnemies(false);
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