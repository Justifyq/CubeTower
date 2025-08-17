using CubeTower.UserInterfaces.Elements;
using CubeTower.UserInterfaces.Screens;

namespace CubeTower.Data.Configs.Interfaces
{
    public interface IUIConfiguration
    {
        GameScreen GameScreen { get; }
        PopupScreenUI PopupScreen { get; }
        CubeUIItem CubeItemPrefab { get; }
    }
}