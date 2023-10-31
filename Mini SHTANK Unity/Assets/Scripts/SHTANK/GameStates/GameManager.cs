using System;
using UnityEngine;
using Utility;
using Utility.StateMachine;

namespace SHTANK.GameStates
{
    public class GameManager : Singleton<GameManager>
    {
        public event Action EnteringCombatState;
        public event Action EnteringOverworldState;
        
        [SerializeField] private OverworldState _overworldState;
        [SerializeField] private CombatState_Start _combatStateStart;
        
        private StateMachine _stateMachine;

        protected override void Awake()
        {
            base.Awake();

            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {
            _overworldState.SetManager(this);
            _combatStateStart.SetManager(this);

            _stateMachine = new StateMachine(_overworldState,
                new Connection(_overworldState, _combatStateStart),
                new Connection(_combatStateStart, _overworldState));
        }

        private void Update()
        {
            _stateMachine.CurrentState.ProcessState();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.ProcessStateFixed();
        }

        public bool TryEnterCombatState()
        {
            bool result = _stateMachine.TryChangeState(_combatStateStart);

            if (!result)
                return false;

            EnteringCombatState?.Invoke();
            return true;
        }

        public void EnterOverworldState()
        {
            if (!_stateMachine.TryChangeState(_overworldState))
                return;
            
            EnteringOverworldState?.Invoke();
        }
    }
}
