using CubeTower.Data.Configs;
using CubeTower.Data.Configs.Interfaces;
using CubeTower.Factories;
using UnityEngine;
using Zenject;

namespace CubeTower.Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private Configuration config;
        
        public override void InstallBindings()
        {
            Container.Bind<IConfiguration>().FromInstance(config).AsSingle().NonLazy();
            Container.Bind<ICubesFactory>().To<CubesFactory>().FromNew().AsSingle().NonLazy();
            Container.Bind<IMapFactory>().To<MapFactory>().FromNew().AsSingle().NonLazy();
            Container.Bind<IUIFactory>().To<UIFactory>().FromNew().AsSingle().NonLazy();
        }
    }
}