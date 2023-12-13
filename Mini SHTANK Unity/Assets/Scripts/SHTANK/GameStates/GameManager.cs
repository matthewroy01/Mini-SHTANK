using System;
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
        [Space]
        [SerializeField] private InstructionPopup _instructionPopup;
        
        private StateMachine _stateMachine;

        private void OnEnable()
        {
            _combatStateStart.DoneWithAnimation += CombatStateStart_OnDoneWithAnimation;
            _combatStateSelect.DoneSelecting += CombatStateSelect_OnDoneSelecting;
            _combatStateProcessAbilities.DoneProcessingAbilities += CombatStateProcessAbilities_OnDoneProcessingAbilities;
            _combatStateEnemyTurn.DoneWithEnemyTurn += CombatStateEnemyTurn_OnDoneWithEnemyTurn;
        }

        private void OnDisable()
        {
            _combatStateStart.DoneWithAnimation -= CombatStateStart_OnDoneWithAnimation;
            _combatStateSelect.DoneSelecting -= CombatStateSelect_OnDoneSelecting;
            _combatStateProcessAbilities.DoneProcessingAbilities -= CombatStateProcessAbilities_OnDoneProcessingAbilities;
            _combatStateEnemyTurn.DoneWithEnemyTurn -= CombatStateEnemyTurn_OnDoneWithEnemyTurn;
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

        private void CombatStateEnemyTurn_OnDoneWithEnemyTurn()
        {
            _stateMachine.TryChangeState(_combatStateSelect);
        }

        protected override void Awake()
        {
            base.Awake();

            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {
            _overworldState.SetManager(this);
            _combatStateStart.SetManager(this);
            _combatStateSelect.SetManager(this);
            _combatStateProcessAbilities.SetManager(this);
            _combatStateEnemyTurn.SetManager(this);

            _stateMachine = new StateMachine(_overworldState,
                new Connection(_overworldState, _combatStateStart),
                new Connection(_combatStateStart, _combatStateSelect),
                new Connection(_combatStateSelect, _combatStateProcessAbilities),
                new Connection(_combatStateProcessAbilities, _combatStateEnemyTurn),
                new Connection(_combatStateEnemyTurn, _combatStateSelect));
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
