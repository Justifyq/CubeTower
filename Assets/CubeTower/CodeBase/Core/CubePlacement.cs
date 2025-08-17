using System.Collections.Generic;
using UnityEngine;

namespace CubeTower.Core
{
    public class CubePlacement : ICubePlacement
    {
        public CubePlacement(Map map, ITower tower, CubesMovement cubesMovement)
        {
            _map = map;
            _tower = tower;
            _cubesMovement = cubesMovement;
        }
        
        private readonly Map _map;
        private readonly ITower _tower;

        private readonly CubesMovement _cubesMovement;
        
        public void Place(CubeView cube, Vector2 pos)
        {
            cube.gameObject.SetActive(true);

            bool canAttachToTower = _tower.WillAttachSuccess(cube, pos);

            if (canAttachToTower)
            {
                _tower.AttachToTower(cube, pos);
                _cubesMovement.MoveCube(cube);
            }
            else
            {
                bool inTrash = _map.TrashSide.bounds.Contains(pos);
           
                if (inTrash)
                {
                    MoveToTrash(cube);
                    MoveUpperCubes(cube);
                    TryDeleteWrongCubes();
                }
                else
                {
                    if (cube.Data.Placed)
                        return;
                    
                    cube.SetSelected(false);
                    _cubesMovement.MoveWrong(cube, pos);
                }
            }
            
        }

        private void MoveToTrash(CubeView cube)
        {
            _tower.Delete(cube);
            _cubesMovement.MoveToTrash(cube, _map.Trash.bounds);
        }

        private void MoveUpperCubes(CubeView cube)
        {
            IEnumerable<CubeView> flyingCubes = _tower.GetSlice(cube.Data.GroundIndex);

            int i = 0;
            foreach (CubeView cv in flyingCubes)
            {
                _cubesMovement.MoveCube(cv, i++);
            }
        }

        private void TryDeleteWrongCubes()
        {
            IEnumerable<CubeView> fallingCubes = _tower.GetFallenCubes();
                    
            int i = 0;
            foreach (CubeView c in fallingCubes)
            {
                _cubesMovement.FallCubeWithCallback(c, _map.Ground, i++);
                _tower.Delete(c);
            }
        }
    }
}