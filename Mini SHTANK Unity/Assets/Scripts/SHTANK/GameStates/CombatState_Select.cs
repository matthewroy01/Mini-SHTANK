using SHTANK.Cards;
using SHTANK.Grid;
using SHTANK.Input;
using UnityEngine;
using Utility.StateMachine;

namespace SHTANK.GameStates
{
    public class CombatState_Select : ManagerState<GameManager>
    {
        [SerializeField] private CardManager _cardManager;
        [Space]
        [SerializeField] private LayerMask _gridSpaceLayerMask;
        [SerializeField] private GameObject _cursor;
        [Space]
        [SerializeField] private string _instructionText;
        private MouseResult<GridSpaceObject> _mouseResult;
        private GridSpaceObject _currentGridSpaceObject;
        
        // TODO: enable hands of cards and movement selection
        
        public override void EnterState()
        {
            Manager.UpdateInstructionPopup(_instructionText);
            _cardManager.DrawCards();
        }

        public override void ExitState()
        {
            Manager.UpdateInstructionPopup();
        }

        public override void ProcessState()
        {
            
        }

        public override void ProcessStateFixed()
        {
            UpdateCurrentGridSpaceObject();
        }

        private void UpdateCurrentGridSpaceObject()
        {
            _mouseResult = MouseManager.Instance.TryGetMousedOverObject<GridSpaceObject>(_gridSpaceLayerMask);

            if (_mouseResult.Hit && _GridSpaceTypeIsValid())
            {
                if (_currentGridSpaceObject == _mouseResult.Component)
                    return;
                
                // TODO: grid space object was selected
                _NewGridSpaceSelected();
            }
            else
            {
                if (_currentGridSpaceObject == null)
                    return;
                
                // TODO: grid space object was deselected
                _GridSpaceDeselected();
            }

            void _NewGridSpaceSelected()
            {
                _currentGridSpaceObject = _mouseResult.Component;
                _cursor.SetActive(true);
                _cursor.transform.position = _currentGridSpaceObject.transform.position;
            }

            void _GridSpaceDeselected()
            {
                _currentGridSpaceObject = null;
                _cursor.SetActive(false);
            }

            bool _GridSpaceTypeIsValid()
            {
                return _mouseResult.Component.GridSpaceType is not (GridSpaceType.None or GridSpaceType.NoMansLand);
            }
        }
    }
}