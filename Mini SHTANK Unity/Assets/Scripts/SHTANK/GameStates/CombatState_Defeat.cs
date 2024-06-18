using System;
using SHTANK.Cards;
using SHTANK.Combat;
using UnityEngine;
using UnityEngine.UI;
using Utility.StateMachine;

namespace SHTANK.GameStates
{
    public class CombatState_Defeat : ManagerState<GameManager>
    {
        public event Action DefeatFinished;

        [SerializeField] private Button _continueButton;

        private CombatResolutionManager _combatResolutionManager;
        private CombatManager _combatManager;
        private bool _finish = false;

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        }

        private void OnContinueButtonClicked()
        {
            _finish = true;
        }

        private void Awake()
        {
            _combatResolutionManager = CombatResolutionManager.Instance;
        }

        public override void EnterState()
        {
            Manager.UpdateInstructionPopup();

            CardManager.Instance.ReturnCardsToDeck();
            _combatResolutionManager.StartCombatResolution(new CombatResolutionInfo(true, null, null));

            _continueButton.interactable = false;
        }

        public override void ExitState()
        {
            _combatManager.EndCombat();
        }

        public override void ProcessState()
        {
            if (!_finish)
                return;

            _continueButton.interactable = false;
            _finish = false;

            _combatResolutionManager.EndCombat(() => DefeatFinished?.Invoke());
        }

        public override void ProcessStateFixed()
        {
            
        }
    }
}