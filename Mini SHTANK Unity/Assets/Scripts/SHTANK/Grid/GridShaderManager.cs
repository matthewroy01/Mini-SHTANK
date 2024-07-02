using SHTANK.Overworld;
using UnityEngine;

namespace SHTANK.Grid
{
    public class GridShaderManager : MonoBehaviour
    {
        [SerializeField] private Level _level;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private bool _yIsZero;

        private static readonly int _enemyPosition0Property = Shader.PropertyToID("_EnemyPosition0");
        private static readonly int _enemyPosition1Property = Shader.PropertyToID("_EnemyPosition1");
        private static readonly int _enemyPosition2Property = Shader.PropertyToID("_EnemyPosition2");
        private static readonly int _enemyPosition3Property = Shader.PropertyToID("_EnemyPosition3");
        private static readonly int _enemyPosition4Property = Shader.PropertyToID("_EnemyPosition4");
        private static readonly int _enemyPosition5Property = Shader.PropertyToID("_EnemyPosition5");
        private static readonly int _enemyDistanceToPlayer0Property = Shader.PropertyToID("_EnemyDistanceToPlayer0");
        private static readonly int _enemyDistanceToPlayer1Property = Shader.PropertyToID("_EnemyDistanceToPlayer1");
        private static readonly int _enemyDistanceToPlayer2Property = Shader.PropertyToID("_EnemyDistanceToPlayer2");
        private static readonly int _enemyDistanceToPlayer3Property = Shader.PropertyToID("_EnemyDistanceToPlayer3");
        private static readonly int _enemyDistanceToPlayer4Property = Shader.PropertyToID("_EnemyDistanceToPlayer4");
        private static readonly int _enemyDistanceToPlayer5Property = Shader.PropertyToID("_EnemyDistanceToPlayer5");

        public void UpdateGridVisuals()
        {
            int positionPropertyID = 0;
            int distancePropertyID = 0;

            for (int i = 0; i < _level.EnemyList.Count; ++i)
            {
                positionPropertyID = i switch
                {
                    0 => _enemyPosition0Property,
                    1 => _enemyPosition1Property,
                    2 => _enemyPosition2Property,
                    3 => _enemyPosition3Property,
                    4 => _enemyPosition4Property,
                    5 => _enemyPosition5Property,
                    _ => positionPropertyID
                };

                distancePropertyID = i switch
                {
                    0 => _enemyDistanceToPlayer0Property,
                    1 => _enemyDistanceToPlayer1Property,
                    2 => _enemyDistanceToPlayer2Property,
                    3 => _enemyDistanceToPlayer3Property,
                    4 => _enemyDistanceToPlayer4Property,
                    5 => _enemyDistanceToPlayer5Property,
                    _ => distancePropertyID
                };

                Vector3 enemyPosition = _level.EnemyList[i].transform.position;

                if (_yIsZero)
                    enemyPosition.y = 0.0f;

                float distanceFromEnemyToPlayer = Vector3.Distance(_playerMovement.transform.position, enemyPosition);

                Shader.SetGlobalVector(positionPropertyID, enemyPosition);
                Shader.SetGlobalFloat(distancePropertyID, distanceFromEnemyToPlayer);
            }

        }
    }
}