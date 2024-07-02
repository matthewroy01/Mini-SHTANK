using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Utility;
using System;

namespace SHTANK.Overworld
{
    public class Level : Singleton<Level>
    {
        public List<Enemy> EnemyList => _enemyList;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private List<Enemy> _enemyList = new();

        protected override void Awake()
        {
            base.Awake();

            Load();
        }

        private void Load()
        {
            _enemyManager.Initialize(_enemyList);
        }

        [Button("Update Enemy List")] [ContextMenu("Update Enemy List")]
        public void UpdateEnemyList()
        {
            _enemyList = new List<Enemy>(FindObjectsOfType<Enemy>());
        }
    }
}
