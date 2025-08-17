using System.Threading;
using CubeTower.Data;
using CubeTower.Factories;
using CubeTower.Infrastructure.DataManagament;
using Cysharp.Threading.Tasks;

namespace CubeTower.Core.States
{
    public class LoadProgressState : IExitableState
    {
        public LoadProgressState(IDataReader dataReader, GameStateMachine stateMachine, ICubesFactory cubesFactory, CubesMovement cubesMovement, ITower tower)
        {
            _tower = tower;
            _dataReader = dataReader;
            _stateMachine = stateMachine;
            _cubesFactory = cubesFactory;
            _cubesMovement = cubesMovement;
        }


        private readonly ITower _tower;
        private readonly IDataReader _dataReader;
        private readonly GameStateMachine _stateMachine;
        private readonly ICubesFactory _cubesFactory;
        private readonly CubesMovement _cubesMovement;
        
        private CancellationTokenSource _cts;
        
        
        public void Enter()
        {
            _cts = new CancellationTokenSource();
            _ = Load();
        }

        public void Exit()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        private async UniTaskVoid Load()
        {
            SavedData d = await _dataReader.Read(_cts.Token);
            
            BuildTower(d.Tower);
            
            _stateMachine.Enter<CubeSearchState>();
        }

        private void BuildTower(CubeViewModel[] cubes)
        {
            if (cubes == null || cubes.Length == 0)
                return;
            
            for (var i = 0; i < cubes.Length; i++)
            {
                CubeView c = _cubesFactory.SpawnCube(cubes[i]);
                _tower.AttachToTower(c);
                _cubesMovement.MoveFromGround(c, i);
            }
            
        }
        
    }
}