using System;
using CubeTower.UserInterfaces.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace CubeTower.UserInterfaces.Screens
{
    public class PopupScreenUI : UIScreen
    {
        [SerializeField] private TextLabel messageLabel;
        [SerializeField] private TextLabel buttonLabel;
        [SerializeField] private Button button;
        [SerializeField] private Color defaultTextColor = Color.red;
        
        private Action _executeAction;

        private void Awake() => button.onClick.AddListener(OnClick);

        private void OnDestroy() => button.onClick.RemoveListener(OnClick);

        public void Assert(string message, string buttonText = "ok", Action onClick = null, Color messageColor = default)
        {
            messageLabel.SetText(message);
            buttonLabel.SetText(buttonText);
            
            messageLabel.Component.color = messageColor == default ? defaultTextColor : messageColor;
            _executeAction = onClick;
            
            Show();
        }

        private void OnClick()
        {
            if (_executeAction != null)
                _executeAction();
            else
                Hide();
        }
    }
}