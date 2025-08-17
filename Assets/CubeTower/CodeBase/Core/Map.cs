using CubeTower.Factories;
using CubeTower.UIlts;
using CubeTower.UserInterfaces;
using CubeTower.UserInterfaces.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace CubeTower.Core
{
    public class Map
    {
        public Map(IMapFactory mapFactory, UI ui)
        {
            _mapFactory = mapFactory;
            _ui = ui;
        }
        
        private Camera _cam;

        private readonly UI _ui;
        private readonly IMapFactory _mapFactory;
        
        public SpriteRenderer TrashSide { get; private set; }
        public SpriteRenderer BuildSide { get; private set; }
        public SpriteRenderer Trash { get; private set; }
        public float Ground { get; private set; }
        public Bounds Bounds { get; private set; }
        
        public void Build()
        {
            _cam = _mapFactory.CreateCamera(out (float width, float height) size);
        
            Trash = _mapFactory.CreateTrash(_cam);
        
            TrashSide = _mapFactory.CreateTrashPart(size.width, size.height);
            BuildSide = _mapFactory.CreateBuildPart(size.width, size.height);
        
            Ground = CalculateGroundPos();
        
            Bounds = new Bounds(_cam.transform.position.ClearZ(), new Vector3(size.width, size.height, 0));
        }
        
        public Vector2 GetWorldPoint(Vector2 screenPoint) => _cam.ScreenToWorldPoint(screenPoint);

        public float CalculateCubeHeight(CubeView cube)
        {
            Sprite sprite = cube.SpriteRenderer.sprite;
            Bounds bound = cube.Bounds;
        
            float pivotY = sprite.pivot.y / sprite.rect.height;
        
            float groundedPos = Ground + (bound.size.y * pivotY) + cube.Bounds.size.y * cube.Data.GroundIndex;
        
            return groundedPos;
        }
        
        private float CalculateGroundPos()
        {
            GameScreen gs = _ui.Get<GameScreen>();
            RectTransform bg = gs.BgPanel;
            CanvasScaler cs = gs.canvasScaler;
        
            gs.canvas.worldCamera = _cam;
        
            float width = Screen.width / cs.referenceResolution.x;
            float height = Screen.height / cs.referenceResolution.y;
        
            float scaleFactor = Mathf.Lerp(width, height, cs.matchWidthOrHeight);
        
            return GetWorldPoint(new Vector2(0, bg.rect.height * scaleFactor)).y;
        }
    }
}