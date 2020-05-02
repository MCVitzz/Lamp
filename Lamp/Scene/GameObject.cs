using Lamp.Models;
using Lamp.Rendering.Camera;
using Lamp.Scene.Lights;
using Lamp.Shaders;

namespace Lamp.Scene
{
    public abstract class GameObject
    {
        public Transform Transform { get; set; }
        protected Model Model;

        public GameObject()
        {
            Transform = new Transform();
        }

        public virtual void Draw()
        {
            Model.Draw();
        }
    }
}
