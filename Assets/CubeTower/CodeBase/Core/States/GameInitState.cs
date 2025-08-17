using CubeTower.UserInterfaces;
using CubeTower.UserInterfaces.Screens;

namespace CubeTower.Core.States
{
    public class GameInitState : IState
    {
        public GameInitState(Map map, GameStateMachine sm, UI ui)
        {
            _map = map;
            
            _stateMachine = sm;
            _ui = ui;
        }

        private readonly GameStateMachine _stateMachine;
        private readonly UI _ui;
        private readonly Map _map;

        
        public void Enter()
        {
            _ui.Build();
            _map.Build();
            
            _ui.Get<GameScreen>().Show();
            _ui.Get<PopupScreenUI>().Hide();
            _stateMachine.Enter<LoadProgressState>();
        }
    }
}