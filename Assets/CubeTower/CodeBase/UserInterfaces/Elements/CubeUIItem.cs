using CubeTower.Core;
using CubeTower.Data;
using CubeTower.UIlts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace CubeTower.UserInterfaces.Elements
{
    public class CubeUIItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image view;
        
        private Cube _cube;
        private ScrollRect _scroll;
        private ICubeCatcher _cubeCatcher;
        
        [Inject]
        private void Configure(ICubeCatcher catcher) => _cubeCatcher = catcher;

        public void Configure(Cube c, ScrollRect sr)
        {
            view.sprite = c.sprite;
            _cube = c;
            _scroll = sr;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _scroll.enabled = false;
            _cubeCatcher.Catch(_cube, eventData.position);
        }

        //TODO: fix when try scroll from this element
        public void OnPointerUp(PointerEventData eventData) => _scroll.enabled = true;
    }
}