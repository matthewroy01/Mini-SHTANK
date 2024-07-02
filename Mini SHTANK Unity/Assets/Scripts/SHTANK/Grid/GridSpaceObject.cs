using System;
using NaughtyAttributes;
using UnityEngine;

namespace SHTANK.Grid
{
    public class GridSpaceObject : MonoBehaviour
    {
        public GridConnections GridConnections => _gridConnections;
        public GridSpaceType GridSpaceType => _gridSpaceType;
        public string ID => _id;
        public Vector3Int IntWorldPosition => _intWorldPosition;

        [SerializeField] private GridSpaceVisual _gridSpaceVisual;
        [Space]
        [Header("Pre-Runtime Data")]
        [ReadOnly] [SerializeField] private GridConnections _gridConnections;
        [ReadOnly] [SerializeField] private GridSpaceType _gridSpaceType = GridSpaceType.None;
        [ReadOnly] [SerializeField] private string _id;
        [ReadOnly] [SerializeField] private Vector3Int _intWorldPosition;

        public void UpdateWithNewGridSpaceType(GridSpaceType gridSpaceType)
        {
            _gridSpaceVisual.SetColor(GridManager.Instance.GetGridSpaceColor(gridSpaceType));
        }

        public void UpdateGameObjectName()
        {
            gameObject.name = _id + (_gridSpaceType == GridSpaceType.None ? "" : " " + Enum.GetName(typeof(GridSpaceType), _gridSpaceType));
        }

        public void SetGridConnections(GridConnections gridConnections)
        {
            _gridConnections = gridConnections;
        }

        public void SetGridSpaceType(GridSpaceType gridSpaceType)
        {
            if (gridSpaceType != _gridSpaceType)
                UpdateWithNewGridSpaceType(gridSpaceType);

            _gridSpaceType = gridSpaceType;

            UpdateGameObjectName();
        }

        public void SetID(string id)
        {
            _id = id;
        }

        public void SetIntWorldPosition(Vector3Int intWorldPosition)
        {
            _intWorldPosition = intWorldPosition;
        }
    }
}