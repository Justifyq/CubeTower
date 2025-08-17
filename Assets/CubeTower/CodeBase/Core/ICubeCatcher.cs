using System;
using CubeTower.Data;
using UnityEngine;

namespace CubeTower.Core
{
    public interface ICubeCatcher
    {
        event Action<CubeView> OnCatch;
        event Action<CubeView> OnRelease;
        
        CubeView Cube { get; }
        Vector2 CatchPos { get; }
        Vector2 ReleasePos { get; }
        Vector2 ActualPosition { get; }
        void Catch(CubeView cube);
        void Catch(Cube cube, Vector2 pos);
    }
}