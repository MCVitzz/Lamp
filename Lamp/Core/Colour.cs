using OpenTK;

namespace Lamp.Core
{
    public class Colour
    {
        public float R, G, B, A;

        public Colour(float r, float g, float b) : this(r, g, b, 1) { }

        public Colour(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Vector4 ToVector4()
        {
            return new Vector4(R, G, B, A);
        }

        public Vector3 ToVector3()
        {
            return new Vector3(R, G, B);
        }

        public System.Drawing.Color ToDrawing()
        {
            return System.Drawing.Color.FromArgb((int)A * 255, (int)R * 255, (int)G * 255, (int)B * 255);
        }
    }
}
