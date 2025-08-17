using System.Collections.Generic;
using CubeTower.Data.Configs.Interfaces;
using UnityEngine;

namespace CubeTower.Data.Configs
{
    [CreateAssetMenu(fileName = nameof(CubesData), menuName = "CubeTower/Configurations/Cubes", order = 2)]
    public class CubesData : ScriptableObject, ICubesData
    {
        [SerializeField] private Cube[] cubes;
        
        private Dictionary<string, Cube> _nameMap;
        
        public IEnumerable<Cube> Data => cubes;
        
        
        public Cube Get(string n)
        {
            var dict = GetNameDict();
            return dict[n];
        }
        
        private Dictionary<string, Cube> GetNameDict()
        {
            if (_nameMap == null)
            {
                _nameMap = new Dictionary<string, Cube>();
            
                foreach (Cube cube in cubes) 
                    _nameMap.TryAdd(cube.name, cube);
            }
            
            return _nameMap;
        }
        

    }
}