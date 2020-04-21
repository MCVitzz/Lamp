using Lamp.Core;
using OpenTK;

namespace Lamp.Scene.Lights
{
    public abstract class Light
    {
        public Vector3 Position;
        public Colour Colour;
        public Vector2 Bias;
    }
}
