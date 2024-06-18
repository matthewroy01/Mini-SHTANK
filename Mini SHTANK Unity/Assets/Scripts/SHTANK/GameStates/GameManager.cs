using System;
using NaughtyAttributes;
using SHTANK.UI;
using UnityEngine;
using Utility;
using Utility.StateMachine;

namespace SHTANK.GameStates
{
    public class GameManager : Singleton<GameManager>
    {
        public event Action EnteringCombatState;
        public event Action EnteringOverworldState;
        
        [Header("States")]
        [SerializeField] private OverworldState _overworldState;
        [SerializeField] private CombatState_Start _combatStateStart;
        [SerializeField] private CombatState_Select _combatStateSelect;
        [SerializeField] private CombatState_ProcessAbilities _combatStateProcessAbilities;
        [SerializeField] private CombatState_EnemyTurn _combatStateEnemyTurn;
        [SerializeField] private CombatState_Victory _combatStateVictory;
        [SerializeField] private CombatState_Defeat _combatStateDefeat;
        [Space]
        [SerializeField] private InstructionPopup _instructionPopup;
        [ShowNativeProperty] private string _currentStateName => (_stateMachine == null || _stateMachine.CurrentState == null) ? "None" : _stateMachine.CurrentState.StateName;
        
        private StateMachine _stateMachine;

        private void OnEnable()
        {
            _combatStateStart.DoneWithAnimation += CombatStateStart_OnDoneWithAnimation;
            _combatStateSelect.DoneSelecting += CombatStateSelect_OnDoneSelecting;
            _combatStateProcessAbilities.DoneProcessingAbilities += CombatStateProcessAbilities_OnDoneProcessingAbilities;
            _combatStateProcessAbilities.EnemyReachedZeroHealth += CombatStateProcessAbilities_OnEnemyReachedZeroHealth;
            _combatStateEnemyTurn.DoneWithEnemyTurn += CombatStateEnemyTurn_OnDoneWithEnemyTurn;
            _combatStateVictory.VictoryFinished += CombatStateVictory_OnVictoryFinished;
            _combatStateDefeat.DefeatFinished += CombatStateDefeat_OnDefeatFinished;
        }

        private void OnDisable()
        {
            _combatStateStart.DoneWithAnimation -= CombatStateStart_OnDoneWithAnimation;
            _combatStateSelect.DoneSelecting -= CombatStateSelect_OnDoneSelecting;
            _combatStateProcessAbilities.DoneProcessingAbilities -= CombatStateProcessAbilities_OnDoneProcessingAbilities;
            _combatStateProcessAbilities.EnemyReachedZeroHealth -= CombatStateProcessAbilities_OnEnemyReachedZeroHealth;
            _combatStateEnemyTurn.DoneWithEnemyTurn -= CombatStateEnemyTurn_OnDoneWithEnemyTurn;
            _combatStateVictory.VictoryFinished -= CombatStateVictory_OnVictoryFinished;
            _combatStateDefeat.DefeatFinished -= CombatStateDefeat_OnDefeatFinished;
        }

        private void CombatStateStart_OnDoneWithAnimation()
        {
            _stateMachine.TryChangeState(_combatStateSelect);
        }

        private void CombatStateSelect_OnDoneSelecting()
        {
            _stateMachine.TryChangeState(_combatStateProcessAbilities);
        }

        private void CombatStateProcessAbilities_OnDoneProcessingAbilities()
        {
            _stateMachine.TryChangeState(_combatStateEnemyTurn);
        }

        private void CombatStateProcessAbilities_OnEnemyReachedZeroHealth()
        {
            _stateMachine.TryChangeState(_combatStateVictory);
        }

        private void CombatStateEnemyTurn_OnDoneWithEnemyTurn()
        {
            _stateMachine.TryChangeState(_combatStateSelect);
        }

        private void CombatStateVictory_OnVictoryFinished()
        {
            EnterOverworldState();
        }

        private void CombatStateDefeat_OnDefeatFinished()
        {
            EnterOverworldState();
        }

        protected override void Awake()
        {
            base.Awake();

            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {
            _InitializeState(_overworldState, StateNames.OVERWORLD);
            _InitializeState(_combatStateStart, StateNames.COMBAT_START);
            _InitializeState(_combatStateSelect, StateNames.COMBAT_SELECT);
            _InitializeState(_combatStateProcessAbilities, StateNames.COMBAT_PROCESS_ABILITIES);
            _InitializeState(_combatStateEnemyTurn, StateNames.COMBAT_ENEMY_TURN);
            _InitializeState(_combatStateVictory, StateNames.COMBAT_VICTORY);
            _InitializeState(_combatStateDefeat, StateNames.COMBAT_DEFEAT);

            _stateMachine = new StateMachine(_overworldState,
                new Connection(_overworldState, _combatStateStart),
                new Connection(_combatStateStart, _combatStateSelect),
                new Connection(_combatStateSelect, _combatStateProcessAbilities),
                new Connection(_combatStateProcessAbilities, _combatStateEnemyTurn),
                new Connection(_combatStateEnemyTurn, _combatStateSelect),
                new Connection(_combatStateProcessAbilities, _combatStateVictory),
                new Connection(_combatStateEnemyTurn, _combatStateDefeat),
                new Connection(_combatStateVictory, _overworldState));

            void _InitializeState(ManagerState<GameManager> state, string stateName)
            {
                state.SetManager(this);
                state.SetStateName(stateName);
            }
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

        public void UpdateInstructionPopup(string text = "")
        {
            _instructionPopup.TrySlide(text);
        }
    }
}
