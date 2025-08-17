using CubeTower.Core;
using CubeTower.Data;
using UnityEngine;

namespace CubeTower.Factories
{
    public interface ICubesFactory
    {
        void SetMovedPreviewPosition(Vector3 position);
        void SetMovedPreview(Vector2 pos, CubeView reference);
        void SetTowerPreview(CubeView reference, Vector2 pos);
        CubeView SpawnCube(Cube cube, Vector3 pos);
        void HidePreview();
        CubeView SpawnCube(CubeViewModel cube);
    }
}