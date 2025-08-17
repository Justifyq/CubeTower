using UnityEngine;

namespace CubeTower.Core
{
    public interface ICubePlacement
    {
        void Place(CubeView cube, Vector2 pos);
    }
}