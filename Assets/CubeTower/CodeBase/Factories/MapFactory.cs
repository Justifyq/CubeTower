using CubeTower.Data.Configs.Interfaces;
using UnityEngine;
using Camera = UnityEngine.Camera;

namespace CubeTower.Factories
{
    public class MapFactory : IMapFactory
    {
        private const  float TargetWidth = 20;
        private const float MinHeight = 5;
        private const float HeightToSizeRatio = 2f;
        private const float PartCenterDiv = 4;
        private const int PartSortOrder = -10;
    
        public MapFactory(IConfiguration config)
        {
            _mapConfig = config.Map;
        }
    
        private readonly IMapViewConfiguration _mapConfig;

        public SpriteRenderer CreateTrashPart(float width, float height)
        {
            var trashPart = new GameObject("trashPart").AddComponent<SpriteRenderer>();
        
            trashPart.sprite = _mapConfig.TrashPart;
        
            trashPart.drawMode = SpriteDrawMode.Tiled;
            trashPart.size = new Vector2(width / HeightToSizeRatio, height);
        
            trashPart.transform.position = new Vector3(-width / PartCenterDiv, 0, 0);

            trashPart.sortingOrder = PartSortOrder;
            return trashPart;
        }

        public SpriteRenderer CreateBuildPart(float width, float height)
        {
            var buildPart = Instantiate<SpriteRenderer>(name: "buildPart");
            buildPart.sprite = _mapConfig.BuildPart;
        
            buildPart.drawMode = SpriteDrawMode.Tiled;
            buildPart.size = new Vector2(width / HeightToSizeRatio, height);
            buildPart.transform.position = new Vector3(width / PartCenterDiv, 0, 0);
            buildPart.sortingOrder = PartSortOrder;
            return buildPart;
        }
    
        public SpriteRenderer CreateTrash(Camera cam)
        {
            float ortho = cam.orthographicSize;
            float aspect = cam.aspect;
        
            Vector3 cameraPosition = cam.transform.position;
        
            float x = cameraPosition.x + ortho * aspect * _mapConfig.TrashAnchor.x;
            float y = cameraPosition.y + ortho *  _mapConfig.TrashAnchor.y;

            var trash = Instantiate<SpriteRenderer>(name: "trash", position: new Vector2(x, y));
            trash.sprite = _mapConfig.Trash;
            return trash;
        }

        public Camera CreateCamera(out (float width, float height) size)
        {
            var cam = Instantiate<Camera>(position: new Vector3(0, 0, -10));

            size = ConfigureCamera(cam);

            return cam;
        }
    
        private (float width, float height) ConfigureCamera(Camera cam)
        {
            cam.orthographic = true;
            cam.tag = "MainCamera";
        
            float orthoSize = TargetWidth / (HeightToSizeRatio * cam.aspect);
        
            float height = orthoSize * HeightToSizeRatio;

        
            if (height < MinHeight)
            {
                orthoSize = MinHeight / HeightToSizeRatio;
                height = orthoSize * HeightToSizeRatio;
            }
        
            cam.orthographicSize = orthoSize;
        
            float width = height * cam.aspect;

            return (width, height);
        }

        private TComponent Instantiate<TComponent>(string name = nameof(TComponent), Vector3 position = default) where TComponent : Component
        {
            var o = new GameObject(name).AddComponent<TComponent>();
        
            if (position != default)
                o.transform.position = position;
        
            return o;
        }
    }
}