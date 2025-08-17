using System;
using System.Collections.Generic;
using System.Linq;
using CubeTower.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CubeTower.Core
{
    public class Tower : ITower
    {
        private const float MinBoundRange = .25f;
        private const float MaxBoundRange = .75f;

        
        public Tower(Map map)
        {
            _map = map;
            Cubes = new List<CubeView>();
        }
        
        private readonly Map _map;
        
        public int Count => Cubes.Count;
        private List<CubeView> Cubes { get; }
        
        public void AttachToTower(CubeView cube, Vector2 attachPos = default)
        {
          
            float x = cube.Data.Placed ? cube.Data.PosX : GetHorizontalPosition(cube, attachPos);
            int index = cube.Data.Placed ? cube.Data.GroundIndex : Cubes.Count;
            
            cube.SetAttached(index, x);
            
            Cubes.Add(cube);
        }

        public CubeViewModel[] GetCubeTowerData() => Cubes.Select(s => s.Data).ToArray();
        
        public CubeView FindCube(Vector2 pos) => Cubes.FirstOrDefault(c => c.Bounds.Contains(pos));
        
        public Vector3 GetExpectedCubePosition() => Cubes.Count == 0 ? default : GetExpectedCubePosition(Cubes.Last());

        public bool WillAttachSuccess(CubeView cube, Vector2 attachPos)
        {
            Bounds bounds = cube.Bounds;
            bounds.center = attachPos;
            
            return IsFreeSpaceForCube() && !Cubes.Contains(cube) && IsBoundsIntersects(bounds);
        }
        
        public bool IsFreeSpaceForCube()
        {
            if (Count == 0)
                return true;
            
            var last = Cubes.Last();

            bool can = _map.Bounds.max.y > last.Bounds.max.y;

            return can;
        }
        
        public IEnumerable<CubeView> GetSlice(int start) => Cubes.GetRange(start, Cubes.Count - start);
        
        public IEnumerable<CubeView> GetFallenCubes()
        {
            for (int i = 0; i < Cubes.Count - 1; i++)
            {
                var first = Cubes[i];
                var second = Cubes[i + 1];

                if (!Intersects(first.Bounds.min.x, first.Bounds.max.x, second.Bounds.min.x, second.Bounds.max.x))
                {
                    return GetSlice(i + 1);
                }
            }
            
            return Array.Empty<CubeView>();
        }
        
        public void Delete(CubeView cube)
        {
            int index = cube.Data.GroundIndex;

            Cubes.Remove(cube);

            for (int i = index; i < Cubes.Count; i++)
            {
                Cubes[i].SetAttached(i, Cubes[i].Data.PosX);
            }
        }

        private bool IsBoundsIntersects(Bounds bound)
        {
            if (Cubes.Count == 0)
                return bound.min.y > _map.Ground;
            
            //TODO: нужно поправить на просчет по количеству кубов а не по вью
            Bounds b = CalculateBoundsForNewCube(Cubes.Last());

            return b.Intersects(bound);
        }

        private float GetHorizontalPosition(CubeView origin, Vector2 worldPos)
        {
            if (Cubes.Count == 0)
                return worldPos.x;
            
            if (origin.Data.Placed)
                return origin.Data.PosX;
            
            var lc = Cubes.Last();

            float x = Mathf.Lerp(lc.Bounds.min.x, lc.Bounds.max.x, Random.Range(MinBoundRange, MaxBoundRange));

            return x;
        }

        private Bounds CalculateBoundsForNewCube(CubeView cube) => CalculateBoundsForNew(cube.Bounds);

        private Bounds CalculateBoundsForNew(Bounds bounds)
        {
            //TODO: нужно поправить на просчет по количеству кубов а не по вью
            CubeView lastCube = Cubes.Last();
            
            Vector3 center = lastCube.Bounds.center;
            center.y += bounds.size.y;
            
            return new Bounds(center, bounds.size);
        }
        
        private Vector3 GetExpectedCubePosition(CubeView lastCube)
        {
            //TODO: нужно поправить на просчет по количеству кубов а не по вью
            var upperPos = lastCube.transform.position;
            upperPos.y += lastCube.Bounds.size.y;
            return upperPos;
        }
        
        private bool Intersects(float xMin1, float xMax1, float xMin2, float xMax2) => xMin1 <= xMax2 && xMin2 <= xMax1;
    }
}