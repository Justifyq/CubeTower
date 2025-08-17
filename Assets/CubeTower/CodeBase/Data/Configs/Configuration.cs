using CubeTower.Data.Configs.Interfaces;
using UnityEngine;

namespace CubeTower.Data.Configs
{
    [CreateAssetMenu(fileName = nameof(Configuration), menuName = "CubeTower/Configurations/GlobalConfig", order = 0)]
    public class Configuration : ScriptableObject, IConfiguration
    {
        [SerializeField] private UIConfig uiConfig;
        [SerializeField] private MapConfiguration mapConfig;
        [SerializeField] private CubesData cubesData;
        
        public IMapViewConfiguration Map => mapConfig;
        public ICubesData Cubes => cubesData;
        public IUIConfiguration UI => uiConfig;
    }
}