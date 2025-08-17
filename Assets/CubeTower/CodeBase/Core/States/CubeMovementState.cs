using CubeTower.Factories;

namespace CubeTower.Core.States
{
    public class CubeMovementState : IUpdateableState, IExitableState
    {
        public CubeMovementState(ITower tower, GameStateMachine stateMachine, ICubePlacement placement, ICubesFactory cubesFactory, ICubeCatcher cubeCatcher)
        {
            _tower = tower;
            _stateMachine = stateMachine;
            _placement = placement;
            _cubesFactory = cubesFactory;
            _cubeCatcher = cubeCatcher;
        }
        
        private readonly ITower _tower;
        private readonly GameStateMachine _stateMachine;
        private readonly ICubePlacement _placement;
        private readonly ICubesFactory _cubesFactory;
        private readonly ICubeCatcher _cubeCatcher;
        
        public void Enter()
        {
            _cubeCatcher.OnRelease += CubeCatcher_OnRelease;

            bool canBuild = _tower.IsFreeSpaceForCube();

            if (_cubeCatcher.Cube.Data.Placed || canBuild) 
                ConfigurePreview();
        }
        
        public void Exit()
        {
            _cubesFactory.HidePreview();
            _cubeCatcher.OnRelease -= CubeCatcher_OnRelease;
        }

        public void Update()
        {
            _cubesFactory.SetMovedPreviewPosition(_cubeCatcher.ActualPosition);
        }

        private void CubeCatcher_OnRelease(CubeView cube)
        {
            _placement.Place(cube, _cubeCatcher.ReleasePos);
            _stateMachine.Enter<SaveProgressState>();
        }
        
        private void ConfigurePreview()
        {
           _cubesFactory.SetMovedPreview(_cubeCatcher.ActualPosition, _cubeCatcher.Cube);
            
            if (!_cubeCatcher.Cube.Data.Placed && _tower.Count > 0) 
                _cubesFactory.SetTowerPreview(_cubeCatcher.Cube, _tower.GetExpectedCubePosition());

           
        }
        
    }
}