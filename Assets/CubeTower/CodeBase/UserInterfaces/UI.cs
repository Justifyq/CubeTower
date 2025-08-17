using System;
using System.Collections.Generic;
using CubeTower.Factories;
using CubeTower.UserInterfaces.Screens;

namespace CubeTower.UserInterfaces
{
    public class UI
    {
        public UI(IUIFactory factory) => _factory = factory;
        
        private readonly IUIFactory _factory;
        private Dictionary<Type, UIScreen> _screens;
        
        public void Build()
        {
            if (_screens != null)
                return;
            
            _screens = new Dictionary<Type, UIScreen>
            {
                { typeof(GameScreen), _factory.CreateGameScreen() },
                { typeof(PopupScreenUI), _factory.CreatePopup() }
            };
        }

        public TScreen Get<TScreen>() where TScreen : UIScreen
        {
            if (!_screens.ContainsKey(typeof(TScreen)))
                return null;
            
            return _screens[typeof(TScreen)] as TScreen;
        }
    }
}