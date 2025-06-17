using System.Collections;
using UnityEngine;


namespace UIElement
{
    public static class UIE
    {
        public static Vector2 WorldToScreen(Vector2 pos, Vector2 size, RectTransform rect, int mult)
        {
           

            return new Vector2(
                // X
                (
                    (mult *size.x / 2)
                    + (pos.x)
                    + ((rect.rect.width / 2) * mult)
                ),

                // Y
                (
                    (pos.y)
                )
            );
        }

    }
}