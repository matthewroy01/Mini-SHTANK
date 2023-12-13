using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

namespace SHTANK.Input
{
    public class MouseManager : Singleton<MouseManager>
    {
        private Ray _ray;
        private RaycastHit _raycastHit;
        private Camera _camera;
        private Mouse _mouse;
        
        protected override void Awake()
        {
            base.Awake();
            
            _camera = Camera.main;
            _mouse = Mouse.current;
        }
        
        public MouseResult<T> TryGetMousedOverObject<T>(LayerMask layerMask) where T : Component
        {
            _ray = _camera.ScreenPointToRay(GetMousePosition());

            Physics.Raycast(_ray, out _raycastHit, float.MaxValue, layerMask);

            if (_raycastHit.transform == null)
                return new MouseResult<T>(false);
            
            T component = _raycastHit.transform.GetComponentInParent<T>();

            return new MouseResult<T>(true, component);
        }

        private Vector2 GetMousePosition()
        {
            return _mouse.position.ReadValue();
        }
    }
}