using Lamp.Rendering.Buffers;
using Lamp.Rendering.Camera;
using Lamp.Scene;
using Lamp.Scene.Lights;
using Lamp.Shaders;

namespace Lamp.GUI
{
    public class Button : GameObject
    {
        new GuiVAO Vao;

        private readonly BufferLayout Layout = new BufferLayout(new BufferElement[] {
            new BufferElement("position", ShaderDataType.VEC3),
        });

        public Button()
        {
            float[] quad = new float[] { -1, 1, -1, -1, 1, 1, 1, -1 };
            Vao = new GuiVAO(quad, Layout);
        }

        public override void Draw(ICamera camera, Light sun)
        {
            Vao.BindAll();
            Vao.Draw();
        }
    }
}
