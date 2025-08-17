using CubeTower.Core.States;
using CubeTower.Factories;
using UnityEngine;
using Zenject;

namespace CubeTower.Installers
{
    public class StatesInstaller : MonoInstaller
    {
        [SerializeField] private GameStateMachine gsm;
    
        public override void InstallBindings()
        {
            Container.Bind<GameInitState>().AsSingle().NonLazy();
            Container.Bind<CubeMovementState>().AsSingle().NonLazy();
            Container.Bind<CubeSearchState>().AsSingle().NonLazy();

            Container.Bind<StatesFactory>().AsSingle().NonLazy();
            Container.Bind<LoadProgressState>().AsSingle().NonLazy();
            Container.Bind<SaveProgressState>().AsSingle().NonLazy();
        
            Container.BindInterfacesAndSelfTo<GameStateMachine>().FromComponentInNewPrefab(gsm).AsSingle().NonLazy();
        
        }
    }
}