using CubeTower.Data.Configs.Interfaces;
using CubeTower.UserInterfaces.Elements;
using CubeTower.UserInterfaces.Screens;
using UnityEngine;

namespace CubeTower.Data.Configs
{
    [CreateAssetMenu(fileName = nameof(UIConfig), menuName = "CubeTower/Configurations/Storage", order = 3)]
    public class UIConfig : ScriptableObject, IUIConfiguration
    {
        [SerializeField] private GameScreen gsPrefab;
        [SerializeField] private CubeUIItem cubePrefab;
        [SerializeField] private PopupScreenUI popupPrefab;
        
        public GameScreen GameScreen => gsPrefab;
        public CubeUIItem CubeItemPrefab => cubePrefab;
        public PopupScreenUI PopupScreen => popupPrefab;
    }
}