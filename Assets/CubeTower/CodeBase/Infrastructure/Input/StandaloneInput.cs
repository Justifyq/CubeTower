using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CubeTower.Infrastructure.Input
{
    public class StandaloneInput : IInputSystem, IDisposable
    {
        public StandaloneInput()
        {
            _controls = new InputSystemControls();

            _controls.Standalone.Press.started += Press_OnStarted;
            _controls.Standalone.Press.canceled += Press_OnCanceled;
        
            _controls.Enable();
        }
        
        public event Action<Vector2> OnPress;
        public event Action<Vector2> OnRelease;
        
        private readonly InputSystemControls _controls;
        
        public bool Pressed { get; private set; }
        public Vector2 PointerPos => _controls.Standalone.Position.ReadValue<Vector2>();

        private void Press_OnStarted(InputAction.CallbackContext context)
        {
            Pressed = true;
            OnPress?.Invoke(PointerPos);
        }

        private void Press_OnCanceled(InputAction.CallbackContext context)
        {
            Pressed = false;
            OnRelease?.Invoke(PointerPos);
        }

        public void Dispose()
        {
            _controls.Mobile.Press.started -= Press_OnStarted;
            _controls.Mobile.Press.canceled -= Press_OnCanceled;
            _controls.Disable();
        }
        
        ~StandaloneInput() => Dispose();
    }
}