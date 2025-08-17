using UnityEngine;
using UnityEngine.UI;

namespace CubeTower.UserInterfaces.Screens
{
    [RequireComponent(typeof(Canvas), typeof(CanvasScaler))]
    public abstract class UIScreen : MonoBehaviour
    {
        public Canvas canvas;
        public CanvasScaler canvasScaler;
        
        public void Show() => canvas.gameObject.SetActive(true);

        public void Hide() => canvas.gameObject.SetActive(false);

        protected virtual void OnValidate()
        {
            if (canvas == null && TryGetComponent<Canvas>(out var c))
                canvas = c;

            if (canvasScaler == null && TryGetComponent<CanvasScaler>(out var cs))
                canvasScaler = cs;
        }
    }
}