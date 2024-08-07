﻿using System;
using System.Collections;
using SHTANK.Combat;
using SHTANK.Grid;
using SHTANK.Overworld;
using UnityEngine;
using Utility.StateMachine;

namespace SHTANK.GameStates
{
    public class CombatState_Start : ManagerState<GameManager>
    {
        public event Action DoneWithAnimation;

        [SerializeField] private CombatTransitionHandler _combatTransitionHandler;
        [SerializeField] private CombatManager _combatManager;
        [SerializeField] private Player _player;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private GridShaderManager _gridShaderManager;
        [Header("Durations")]
        [SerializeField] private float _separationDuration;
        [SerializeField] private float _zoomInDuration;
        [SerializeField] private float _holdDuration;
        [SerializeField] private float _zoomOutDuration;

        // TODO: create animation when combat starts

        public override void EnterState()
        {
            Manager.CameraManager.ToggleSplitScreenCamera(true);

            StartCoroutine(CombatStartRoutine());
        }

        public override void ExitState()
        {
            _player.gameObject.SetActive(false);
            _enemyManager.ToggleEnemies(false);
        }

        public override void ProcessState()
        {

        }

        public override void ProcessStateFixed()
        {
            Manager.CameraManager.DoZoomSplitCameraBehavior();
        }

        private IEnumerator CombatStartRoutine()
        {
            Enemy enemy = GameManager.Instance.CombatBeginningInfo.Enemy;
            CombatBeginningInfo combatBeginningInfo = new CombatBeginningInfo(enemy, enemy.transform.position);

            _combatManager.InitializeGrid(combatBeginningInfo);

            if (enemy != null)
            {
                bool flipped = _player.transform.position.x > enemy.transform.position.x;

                enemy.PlayAlertParticleSystem();

                yield return _combatTransitionHandler.SeparateOverworldPlayerAndEnemy(_separationDuration, _player, enemy, flipped, _combatManager.EnemyGridSpaceObject.transform.position);

                yield return _combatTransitionHandler.ZoomInCameras(_zoomInDuration, _player, enemy);

                _combatTransitionHandler.DoVSEffect();

                yield return new WaitForSeconds(_holdDuration);

                _gridShaderManager.SetSquareMode(true);
                _gridShaderManager.UpdateGridVisuals();

                yield return _combatTransitionHandler.ZoomOutCameras(_zoomOutDuration, Manager.CameraManager.CombatCameraStateParameters);
            }

            _combatManager.BeginCombat(combatBeginningInfo);

            DoneWithAnimation?.Invoke();
        }
    }
}