using SHTANK.Overworld;
using UnityEngine;

namespace SHTANK.Combat
{
    public class CombatBeginningInfo
    {
        public Enemy Enemy => _enemy;
        public Vector3 WorldPosition => _worldPosition;

        private Enemy _enemy;
        private Vector3 _worldPosition;

        public CombatBeginningInfo(Enemy enemy, Vector3 worldPosition)
        {
            _enemy = enemy;
            _worldPosition = worldPosition;
        }
    }
}