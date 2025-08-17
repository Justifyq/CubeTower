using CubeTower.Infrastructure.Localization;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CubeTower.UserInterfaces.Elements
{
    [RequireComponent(typeof(Text))]
    public class TextLabel : MonoBehaviour
    {
        [SerializeField] private Text label;
        [SerializeField] private string key = string.Empty;
        
        private ILocalization _localization;
        
        public Text Component => label;

        public void SetText(string text) => label.text = _localization.GetString(text);

        [Inject]
        private void Configure(ILocalization localization)
        {
            _localization = localization;
            
            if (string.IsNullOrEmpty(key))
                return;
            
            SetText(key);
        }

        private void OnValidate() => label = GetComponent<Text>();
    }
}