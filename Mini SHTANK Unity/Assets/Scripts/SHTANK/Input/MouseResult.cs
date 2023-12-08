using UnityEngine;

namespace SHTANK.Input
{
    public class MouseResult<T> where T : Component
    {
        public bool Hit { get; }
        public T Component { get; }

        public MouseResult(bool hit, T component = default)
        {
            Hit = hit;
            Component = component;
        }
    }
}