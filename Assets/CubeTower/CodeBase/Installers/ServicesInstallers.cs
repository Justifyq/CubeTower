using CubeTower.Infrastructure.DataManagament;
using CubeTower.Infrastructure.Input;
using CubeTower.Infrastructure.Localization;
using Zenject;

namespace CubeTower.Installers
{
    public class ServicesInstallers : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputSystem>().To<StandaloneInput>().FromNew().AsSingle().NonLazy();
            Container.Bind<ILocalization>().To<MockLocalization>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DataHandler>().AsSingle().NonLazy();
        }
    }
}