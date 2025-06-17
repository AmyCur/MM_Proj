using UnityEngine;

namespace ColourUtils
{
    /*
        public class Colour255 : Colour
        {

        }

        public class Colour
        {
            public float r;
            public float g;
            public float b;
            public float a;

            public Colour(float r, float g, float b, float a)
            {
                this.r = r;
                this.g = g;
                this.b = b;
                this.a = a;
            }
        }
    */
    public static class Colours
    {



        //* These are useful because they allow me to write colours like a normal person
        #region 255 Conversions
        // Converts a colour from (255,255,255,255) format to (1,1,1,1) format
        public static Color FTS(Color color)
        {
            return new(
                color.r / 255,
                color.g / 255,
                color.b / 255,
                color.a / 255
            );
        }

        // Converts a colour from (1,1,1,1) format to (255,255,255,255) format
        public static Color STF(Color color)
        {
            return new(
                color.r * 255,
                color.g * 255,
                color.b * 255,
                color.a * 255
            );
        }

        public static Color LerpColours(Color a, Color b, float t)
        {
            return new(
                Mathf.Lerp(a.r, b.r, t),
                Mathf.Lerp(a.g, b.g, t),
                Mathf.Lerp(a.b, b.b, t),
                Mathf.Lerp(a.a, b.a, t)
            );
        }

        public static Color LerpAlphas(Color a, Color b, float t)
        {
            return new(
                a.r,
                a.g,
                a.b,
                Mathf.Lerp(a.a, b.a, t)
            );
        }

        #endregion
    }    
}
