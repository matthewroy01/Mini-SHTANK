using System;
using UnityEngine;

namespace SHTANK.Grid
{
    public class GridSpaceObject : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        
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
                    _meshRenderer.material.color = Color.white;
                    break;
            }
        }
    }
}