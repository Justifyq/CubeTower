namespace CubeTower.Core.States
{
    public class CubeSearchState : IExitableState
    {
        
        public CubeSearchState(ICubeCatcher cubeCatcher, GameStateMachine stateMachine)
        {
            _cubeCatcher = cubeCatcher;
            _stateMachine = stateMachine;
        }
        
        private readonly ICubeCatcher _cubeCatcher;
        private readonly GameStateMachine _stateMachine;
        
        public void Enter()
        {
            _cubeCatcher.OnCatch += CubeCatcher_OnCatch;
        }

        private void CubeCatcher_OnCatch(CubeView cube)
        {
            _stateMachine.Enter<CubeMovementState>();
        }


        public void Exit()
        {
            _cubeCatcher.OnCatch -= CubeCatcher_OnCatch;
        }
    }
}