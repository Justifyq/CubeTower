using CubeTower.Data;
using CubeTower.Data.Configs.Interfaces;
using CubeTower.UserInterfaces.Elements;
using CubeTower.UserInterfaces.Screens;
using UnityEngine;
using Zenject;

namespace CubeTower.Factories
{
    public class UIFactory : IUIFactory
    {
        public UIFactory(IConfiguration config, DiContainer container)
        {
            _uiData = config.UI;
            _cubesData = config.Cubes;
            _container = container;
        }
        
        private readonly IUIConfiguration _uiData;
        private readonly DiContainer _container;
        private readonly ICubesData _cubesData;
        
        public GameScreen CreateGameScreen()
        {
            GameScreen gs = Object.Instantiate(_uiData.GameScreen);
            
            SpawnItems(gs);

            return gs;
        }

        public PopupScreenUI CreatePopup() => _container.InstantiatePrefab(_uiData.PopupScreen).GetComponent<PopupScreenUI>();

        private void SpawnItems(GameScreen gs)
        {
            foreach (Cube cube in _cubesData.Data)
            {
                CubeUIItem c = _container.InstantiatePrefab(_uiData.CubeItemPrefab, gs.RootPanel.transform).GetComponent<CubeUIItem>();
                c.Configure(cube, gs.ItemsScroll);
            }
        }

    }
}