using System.Threading;
using CubeTower.Data;
using CubeTower.Infrastructure.DataManagament;
using Cysharp.Threading.Tasks;

namespace CubeTower.Core.States
{
    public class SaveProgressState : IExitableState
    {        
        public SaveProgressState(ITower tower, IDataWriter dataWriter, GameStateMachine gsm)
        {
            _tower = tower;
            _dataWriter = dataWriter;
            _stateMachine = gsm;
        }

        private readonly ITower _tower;
        private readonly IDataWriter _dataWriter;
        private readonly GameStateMachine _stateMachine;
        
        private CancellationTokenSource _cts;
        
        public void Enter()
        {
            _cts = new CancellationTokenSource();
            _ = Write();
        }

        private async UniTaskVoid Write()
        {
            var data = new SavedData()
            {
                Tower = _tower.GetCubeTowerData()
            };
            
            await _dataWriter.Write(data, _cts.Token);
            _stateMachine.Enter<CubeSearchState>();
        }

        public void Exit()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}