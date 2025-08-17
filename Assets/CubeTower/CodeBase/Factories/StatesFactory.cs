using CubeTower.Core.States;
using Zenject;

namespace CubeTower.Factories
{
    public class StatesFactory
    {
        public StatesFactory(DiContainer container) => _container = container;
        
        private readonly DiContainer _container;
        
        public TState CreateState<TState>() where TState : IState => _container.Resolve<TState>();
    }
}