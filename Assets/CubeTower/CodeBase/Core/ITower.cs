using System.Collections.Generic;
using CubeTower.Data;
using UnityEngine;

namespace CubeTower.Core
{
    public interface ITower
    {
        int Count { get; }
        void AttachToTower(CubeView cube, Vector2 attachPos = default);
        CubeViewModel[] GetCubeTowerData();
        CubeView FindCube(Vector2 pos);
        Vector3 GetExpectedCubePosition();
        bool WillAttachSuccess(CubeView cube, Vector2 attachPos);
        bool IsFreeSpaceForCube();
        IEnumerable<CubeView> GetSlice(int start);
        IEnumerable<CubeView> GetFallenCubes();
        void Delete(CubeView cube);
    }
}