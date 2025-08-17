using UnityEngine;

namespace CubeTower.Factories
{
    public interface IMapFactory
    {
        SpriteRenderer CreateTrashPart(float width, float height);
        SpriteRenderer CreateBuildPart(float width, float height);
        SpriteRenderer CreateTrash(Camera cam);
        Camera CreateCamera(out (float width, float height) size);
    }
}