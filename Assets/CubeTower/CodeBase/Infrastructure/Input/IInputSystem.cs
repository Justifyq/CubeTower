using System;
using UnityEngine;

namespace CubeTower.Infrastructure.Input
{
    public interface IInputSystem
    {
        event Action<Vector2> OnPress;
        event Action<Vector2> OnRelease;
        
        bool Pressed { get; }
        Vector2 PointerPos { get; }
    }
}