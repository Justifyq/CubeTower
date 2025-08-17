using CubeTower.Data;
using UnityEngine;

namespace CubeTower.Core
{
    public class CubeView : MonoBehaviour
    {
        public void SetAttached(int groundIndex, float x)
        {
            Data.GroundIndex = groundIndex;
            Data.Placed = true;
            Data.PosX = x;
        }
        
        [SerializeField] private SpriteRenderer spriteRenderer;
    
        private readonly Color _selectedColor = new(1, 1, 1, .5f);
        private readonly Color _normalColor = Color.white;
    
        public Bounds Bounds => spriteRenderer.bounds;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
    
        public CubeViewModel Data { get; private set; }
        
        public void SetSelected(bool selected) => spriteRenderer.color = selected ? _selectedColor : _normalColor;
        
        public CubeView Configure(Cube cube, bool forceEnable = false, CubeViewModel vm = null)
        {
            Data = vm ?? new CubeViewModel(cube);

            spriteRenderer.sprite = cube.sprite;
        
            if (forceEnable)
                gameObject.SetActive(true);
        
            return this;
        }


    }
}