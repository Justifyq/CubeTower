using CubeTower.Core;
using UnityEngine;

namespace CubeTower.Data.Configs.Interfaces
{
    public interface IMapViewConfiguration
    {
        float TargetWidth { get; }
        float MinHeight { get; }
        
        CubeView CubePrefab { get; }
        Sprite TrashPart { get; }
        Sprite BuildPart { get; }
        Sprite Trash { get; }
        Vector2 TrashAnchor { get; }
    }
}