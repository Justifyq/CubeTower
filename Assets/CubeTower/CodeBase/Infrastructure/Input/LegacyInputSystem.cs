using System;
using UnityEngine;

namespace CubeTower.Infrastructure.Input
{
    public class LegacyInputSystem : MonoBehaviour, IInputSystem
    {
        public event Action<Vector2> OnPress;
        public event Action<Vector2> OnRelease;

        public bool Pressed => UnityEngine.Input.GetMouseButton(0);
        
        public Vector2 PointerPos => UnityEngine.Input.mousePosition;

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0)) 
                OnPress?.Invoke(UnityEngine.Input.mousePosition);

            if (UnityEngine.Input.GetMouseButtonUp(0)) 
                OnRelease?.Invoke(UnityEngine.Input.mousePosition);
        }
    }
}