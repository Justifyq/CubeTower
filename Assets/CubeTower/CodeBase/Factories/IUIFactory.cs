using CubeTower.UserInterfaces.Screens;

namespace CubeTower.Factories
{
    public interface IUIFactory
    {
        GameScreen CreateGameScreen();
        PopupScreenUI CreatePopup();
    }
}