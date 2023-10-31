using SHTANK.Data.CombatEntities;
using UnityEngine;

namespace SHTANK.Combat
{
    public class CombatEntity : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private CombatEntityDefinition _combatEntityDefinition;
        
        public void Initialize(CombatEntityDefinition combatEntityDefinition)
        {
            _combatEntityDefinition = combatEntityDefinition;
            
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            if (_combatEntityDefinition.Sprite == null)
                return;

            _spriteRenderer.sprite = _combatEntityDefinition.Sprite;
        }
    }
}