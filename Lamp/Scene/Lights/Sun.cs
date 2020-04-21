using Lamp.Core;
using OpenTK;

namespace Lamp.Scene.Lights
{
    public class Sun : Light
    {
        public Sun(Colour colour)
        {
            Position = new Vector3(144, 144, 0);
            Colour = colour;
            Bias = new Vector2(1, 0);
        }
    }
}
