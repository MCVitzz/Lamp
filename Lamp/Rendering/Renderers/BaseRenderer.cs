using Lamp.Rendering.Camera;
using Lamp.Scene;
using Lamp.Scene.Lights;
using Lamp.Shaders;
using System.Collections.Generic;

namespace Lamp.Rendering.Renderers
{
    public abstract class BaseRenderer<T> where T : GameObject
    {
        protected Shader Shader;

        public BaseRenderer(Shader shader)
        {
            Shader = shader;
        }

        public abstract void Render(List<T> objs, ICamera camera, Light sun);

    }
}
