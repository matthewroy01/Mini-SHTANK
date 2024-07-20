using System;
using System.Collections;
using DG.Tweening;
using SHTANK.Combat;
using SHTANK.Data.CombatEntities;
using SHTANK.GameStates;
using UnityEngine;

namespace SHTANK.Overworld
{
    public class Enemy : MonoBehaviour
    {
        public CombatEntityDefinition CombatEntityDefinition => _combatEntityDefinition;

        [SerializeField] private CombatEntityDefinition _combatEntityDefinition;
        [Header("Transforms")]
        [SerializeField] private Transform _artContainer;
        [SerializeField] private Transform _fallOverParent;

        private bool _dead;
        private Coroutine _deathRoutine;

        private void OnTriggerEnter(Collider other)
        {
            if (_dead)
                return;

            if (other.GetComponent<PlayerMovement>() == null)
                return;

            GameManager.Instance.TryBeginCombat(new CombatBeginningInfo(this, transform.position));
        }

        public void MarkAsDead()
        {
            _dead = true;
        }

        public bool TryKill()
        {
            if (!_dead)
                return false;

            if (_deathRoutine != null)
                StopCoroutine(_deathRoutine);

            _deathRoutine = StartCoroutine(DeathRoutine());

            return true;
        }

        private IEnumerator DeathRoutine()
        {
            _artContainer.DOLocalRotate(Vector3.up * 720.0f, 1.5f, RotateMode.FastBeyond360).SetEase(Ease.OutSine);

            yield return new WaitForSeconds(0.75f);

            _fallOverParent.DOLocalRotate(Vector3.right * 90.0f, 0.75f, RotateMode.FastBeyond360).SetEase(Ease.InQuart);

            yield return new WaitForSeconds(1.5f);

            Destroy(gameObject);
        }
    }
}