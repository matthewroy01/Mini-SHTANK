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
        
        [SerializeField] private MeshRenderer _meshRenderer;
        [Space]
        [Header("Pre-Runtime Data")]
        [ReadOnly] [SerializeField] private GridConnections _gridConnections;
        [ReadOnly] [SerializeField] private GridSpaceType _gridSpaceType = GridSpaceType.None;
        [ReadOnly] [SerializeField] private string _id;
        [ReadOnly] [SerializeField] private Vector3Int _intWorldPosition;
        
        public void UpdateWithNewGridSpaceType(GridSpaceType gridSpaceType)
        {
            switch(gridSpaceType)
            {
                case GridSpaceType.EnemySpace:
                    _meshRenderer.material.color = Color.red;
                    break;
                case GridSpaceType.Battlefield:
                    _meshRenderer.material.color = Color.blue;
                    break;
                case GridSpaceType.Outskirts:
                    _meshRenderer.material.color = Color.black;
                    break;
                case GridSpaceType.None:
                case GridSpaceType.NoMansLand:
                default:
                    _meshRenderer.material.color = Color.gray;
                    break;
            }
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