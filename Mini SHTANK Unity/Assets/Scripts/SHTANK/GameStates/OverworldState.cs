using SHTANK.Grid;
using SHTANK.Overworld;
using UnityEngine;
using Utility;
using Utility.StateMachine;

namespace SHTANK.GameStates
{
    public class OverworldState : ManagerState<GameManager>
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private GridShaderManager _gridShaderManager;

        public override void EnterState()
        {
            _playerMovement.TogglePhysics(true);
            _playerMovement.gameObject.SetActive(true);
            _enemyManager.ToggleEnemies(true);
            _enemyManager.TryKillEnemies();
            _gridShaderManager.SetSquareMode(false);
        }

        public override void ExitState()
        {
            _playerMovement.TogglePhysics(false);
        }

        public override void ProcessState()
        {
            _playerMovement.MyUpdate();
            _gridShaderManager.UpdateGridVisuals();
        }

        public override void ProcessStateFixed()
        {
            _playerMovement.MyFixedUpdate();
            Manager.CameraManager.DoOverworldCameraBehavior();
        }
    }
}