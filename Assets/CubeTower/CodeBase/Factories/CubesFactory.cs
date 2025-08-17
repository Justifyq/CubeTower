using CubeTower.Core;
using CubeTower.Data;
using CubeTower.Data.Configs.Interfaces;
using UnityEngine;
using Zenject;

namespace CubeTower.Factories
{
    public class CubesFactory : ICubesFactory
    {
        public CubesFactory(IConfiguration config, DiContainer container)
        {
            _prefab = config.Map.CubePrefab;

            _cubesData = config.Cubes;
            _container = container;
            
            _towerPreview = InstantiateCube(new Vector3(0, 0, -10));
            _movedPreview = InstantiateCube(new Vector3(0, 0, -10));
            
            _towerPreview.SetSelected(true);
            _movedPreview.SetSelected(false);
        }        
        
        private readonly CubeView _prefab;
        private readonly CubeView _towerPreview;
        private readonly CubeView _movedPreview;
        private readonly DiContainer _container;

        private readonly ICubesData _cubesData;
        
        public void SetMovedPreviewPosition(Vector3 position) => _movedPreview.transform.position = position;
        
        public void SetMovedPreview(Vector2 pos, CubeView reference) => ConfigureFromReference(_movedPreview, reference, pos);

        public void SetTowerPreview(CubeView reference, Vector2 pos) => ConfigureFromReference(_towerPreview, reference, pos);

        public CubeView SpawnCube(Cube cube, Vector3 pos) => InstantiateCube(pos).Configure(cube);
        
        public CubeView SpawnCube(CubeViewModel cube) => InstantiateCube(Vector3.zero).Configure(_cubesData.Get(cube.Name), vm: cube);
        
        public void HidePreview()
        {
            _towerPreview.gameObject.SetActive(false);
            _movedPreview.gameObject.SetActive(false);
        }
        
        private void ConfigureFromReference(CubeView origin, CubeView reference, Vector2 pos) =>
            origin.Configure(_cubesData.Get(reference.Data.Name), true, reference.Data).transform.position = pos;
        
        private CubeView InstantiateCube(Vector3 pos) => _container.InstantiatePrefab(_prefab, pos, Quaternion.identity).GetComponent<CubeView>();

    }
}