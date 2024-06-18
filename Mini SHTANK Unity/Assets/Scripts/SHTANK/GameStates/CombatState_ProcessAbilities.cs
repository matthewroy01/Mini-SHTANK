using System;
using System.Collections;
using SHTANK.Cards;
using SHTANK.Cards.Processing;
using SHTANK.Combat;
using UnityEngine;
using Utility.StateMachine;

namespace SHTANK.GameStates
{
    public class CombatState_ProcessAbilities : ManagerState<GameManager>
    {
        public event Action DoneProcessingAbilities;
        public event Action EnemyReachedZeroHealth;
        
        [SerializeField] private string _instructionText;

        private bool _processingInProgress;
        private Coroutine _processCardsRoutine;
        private float _timer;
        
        public override void EnterState()
        {
            _processingInProgress = true;
            _timer = 0.0f;
            Manager.UpdateInstructionPopup(_instructionText);
            
            if (_processCardsRoutine != null)
                StopCoroutine(_processCardsRoutine);
            
            _processCardsRoutine = StartCoroutine(ProcessAbilitiesRoutine());
        }
        
        public override void ExitState()
        {
            Manager.UpdateInstructionPopup();
        }
        
        public override void ProcessState()
        {
            _timer += Time.deltaTime;
            
            if (_timer > 1.5f && !_processingInProgress)
                DoneProcessingAbilities?.Invoke();
        }
        
        public override void ProcessStateFixed() { }

        private IEnumerator ProcessAbilitiesRoutine()
        {
            yield return CardEffectProcessor.Instance.ProcessCards(CardManager.Instance.QueuedCardInfoStack);

            _processingInProgress = false;

            if (CombatManager.Instance.GetEnemyHealthIsZero())
            {
                EnemyReachedZeroHealth?.Invoke();
            }
        }
    }
}
