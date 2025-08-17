using CubeTower.Core;
using CubeTower.UserInterfaces;
using Zenject;

namespace CubeTower.Installers
{
    public class GameEntitiesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UI>().AsSingle().NonLazy();
            Container.Bind<Map>().AsSingle().NonLazy();
        
            Container.Bind<ICubePlacement>().To<CubePlacement>().AsSingle().NonLazy();
            Container.Bind<ICubeCatcher>().To<CubeCatcher>().AsSingle().NonLazy();
            Container.Bind<ITower>().To<Tower>().AsSingle().NonLazy();
            Container.Bind<CubesMovement>().AsSingle().NonLazy();
        }
    }
}