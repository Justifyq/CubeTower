using System;
using System.Collections.Generic;
using CubeTower.Factories;
using UnityEngine;
using Zenject;

namespace CubeTower.Core.States
{
    public class GameStateMachine : MonoBehaviour, IInitializable
    {
        private IUpdateableState _updateableState;

        private Dictionary<Type, IState> _states;
        
        private IState _activeState;

        [Inject] private StatesFactory _factory;
        
        
        public void Initialize()
        {
            _states = new Dictionary<Type, IState>()
            {
                { typeof(LoadProgressState), _factory.CreateState<LoadProgressState>() },
                { typeof(SaveProgressState), _factory.CreateState<SaveProgressState>() },
                { typeof(GameInitState), _factory.CreateState<GameInitState>() },
                { typeof(CubeMovementState), _factory.CreateState<CubeMovementState>() },
                { typeof(CubeSearchState), _factory.CreateState<CubeSearchState>() }
            };

            Enter<GameInitState>();
        }
        
        public void Update() => _updateableState?.Update();

        public void Enter<TState>() where TState : IState
        {
            if (_activeState is IExitableState state)
                state.Exit();
            
            _activeState =  _states[typeof(TState)];
            
            if (_activeState is IUpdateableState updateableState)
                _updateableState = updateableState;
            else
                _updateableState = null;
            
            _activeState.Enter();
        }
    }
}