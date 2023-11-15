using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SHTANK.Overworld
{
    public class EnemyManager : MonoBehaviour
    {
        private List<Enemy> _enemies = new();
        
        private void Awake()
        {
            _enemies = FindObjectsOfType<Enemy>().ToList();
        }

        public void ToggleEnemies(bool enable)
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.gameObject.SetActive(enable);
            }
        }
    }
}
