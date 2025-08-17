using UnityEngine;

namespace CubeTower.UIlts
{
    public static class Extensions
    {
#if UNITY_EDITOR
        public static void Draw(this Bounds b, float dur = 3)
        {
            Vector2 bottomLeft = b.min;
            Vector2 bottomRight = new Vector2(b.max.x, b.min.y);
            Vector2 topLeft = new Vector2(b.min.x, b.max.y);
            Vector2 topRight = b.max;
            
            Debug.DrawLine(bottomLeft, bottomRight, Color.red, duration: dur);
            Debug.DrawLine(bottomRight, topRight, Color.red, duration: dur);
            Debug.DrawLine(topRight, topLeft, Color.red, duration: dur);
            Debug.DrawLine(topLeft, bottomLeft, Color.red, duration: dur);
        }  
#endif
        
        public static Vector3 ClearZ(this Vector3 v)
        {
            v.z = 0;
            return v;
        }
    }
}