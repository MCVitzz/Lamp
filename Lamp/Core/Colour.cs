using OpenTK;

namespace Lamp.Core
{
    public class Colour
    {
        public float r, g, b, a;

        public Colour(float r, float g, float b) : this(r, g, b, 1) { }

        public Colour(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public Vector4 ToVector4()
        {
            return new Vector4(r, g, b, a);
        }

        public Vector3 ToVector3()
        {
            return new Vector3(r, g, b);
        }
    }
}
