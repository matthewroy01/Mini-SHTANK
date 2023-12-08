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
        [Space]
        [SerializeField] private InstructionPopup _instructionPopup;
        
        private StateMachine _stateMachine;

        private void OnEnable()
        {
            _combatStateStart.DoneWithAnimation += CombatStateStart_OnDoneWithAnimation;
        }

        private void OnDisable()
        {
            _combatStateStart.DoneWithAnimation -= CombatStateStart_OnDoneWithAnimation;
        }

        private void CombatStateStart_OnDoneWithAnimation()
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

            _stateMachine = new StateMachine(_overworldState,
                new Connection(_overworldState, _combatStateStart),
                new Connection(_combatStateStart, _combatStateSelect));
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
