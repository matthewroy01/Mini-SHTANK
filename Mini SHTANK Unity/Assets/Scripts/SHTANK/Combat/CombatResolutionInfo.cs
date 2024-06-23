namespace SHTANK.Combat
{
    public class CombatResolutionInfo
    {
        public bool PlayerVictory => _playerVictory;
        public CombatEntity[] InvolvedPlayerArray => _involvedPlayerArray;
        public CombatEntity[] InvolvedEnemyArray => _involvedEnemyArray;

        private readonly bool _playerVictory;
        private readonly CombatEntity[] _involvedPlayerArray;
        private readonly CombatEntity[] _involvedEnemyArray;

        public CombatResolutionInfo(bool playerVictory, CombatEntity[] involvedPlayerArray, params CombatEntity[] involvedEnemyArray)
        {
            _playerVictory = playerVictory;
            _involvedPlayerArray = involvedPlayerArray;
            _involvedEnemyArray = involvedEnemyArray;
        }
    }
}