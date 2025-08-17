using UnityEngine;
using UnityEngine.UI;

namespace CubeTower.UserInterfaces.Screens
{
    public class GameScreen : UIScreen
    {        
        [SerializeField] private ScrollRect itemsScroll;
        [SerializeField] private RectTransform rootPanel;
        [SerializeField] private RectTransform bgPanel;
        
        public RectTransform RootPanel => rootPanel;
        public RectTransform BgPanel => bgPanel;
        public ScrollRect ItemsScroll => itemsScroll;
    }
}