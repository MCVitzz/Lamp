using Lamp.Core;

namespace Lamp.GUI.Components
{
    public class Container : Component
    {
        public Container(Colour background)
        {
            BackgroundColour = background;
        }

        public Container() : this(new Colour(0.15f, 0.15f, 0.15f, 0.975f))
        {
        }
    }
}
