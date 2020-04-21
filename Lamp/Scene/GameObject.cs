using Lamp.Rendering.Buffers;
using Lamp.Rendering.Camera;
using Lamp.Scene.Lights;
using Lamp.Shaders;

namespace Lamp.Scene
{
    public abstract class GameObject
    {
        public Transform Transform { get; set; }
        protected VAO Vao;
        public Shader Shader;

        public GameObject()
        {
            Transform = new Transform();
        }

        public abstract void Draw(ICamera camera, Light sun);
    }
}
