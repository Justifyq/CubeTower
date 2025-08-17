using CubeTower.Core;
using CubeTower.Data.Configs.Interfaces;
using UnityEngine;

namespace CubeTower.Data.Configs
{
    [CreateAssetMenu(fileName = nameof(MapConfiguration), menuName = "CubeTower/Configurations/Map", order = 1)]
    public class MapConfiguration : ScriptableObject, IMapViewConfiguration
    {
        [SerializeField] private Sprite trashPart;
        [SerializeField] private Sprite buildPart;
        [SerializeField] private CubeView cubePrefab;
        [SerializeField] private Sprite trash;
        [SerializeField] private Vector2 trashAnchors = new Vector2(0.5f, 0);
        [SerializeField] private Vector2 mapSize = new Vector2(20, 5);
        
        public float TargetWidth => mapSize.x;
        public float MinHeight => mapSize.y;
        public CubeView CubePrefab => cubePrefab;
        public Sprite TrashPart => trashPart;
        public Sprite BuildPart => buildPart;
        public Sprite Trash => trash;
        public Vector2 TrashAnchor => trashAnchors;
    }
}