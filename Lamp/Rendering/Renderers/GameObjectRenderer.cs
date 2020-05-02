using Lamp.Rendering.Camera;
using Lamp.Scene;
using Lamp.Scene.Lights;
using Lamp.Shaders;
using System.Collections.Generic;

namespace Lamp.Rendering.Renderers
{
    public class GameObjectRenderer : BaseRenderer<GameObject>
    {
        public GameObjectRenderer() : base(new Shader("Resources/Shaders/Textured/Vertex.glsl", "Resources/Shaders/Textured/Fragment.glsl"))
        {
            Shader.AddUniform("modelMatrix");
            Shader.AddUniform("viewMatrix");
            Shader.AddUniform("projectionMatrix");
            Shader.AddUniform("lightPosition");
            Shader.AddUniform("lightColour");
        }

        public override void Render(List<GameObject> objs, ICamera camera, Light sun)
        {
            foreach (GameObject obj in objs)
            {
                Shader.Bind();
                Shader.UpdateUniform("modelMatrix", obj.Transform.GetMatrix());
                Shader.UpdateUniform("viewMatrix", camera.GetViewMatrix());
                Shader.UpdateUniform("projectionMatrix", camera.GetProjectionMatrix());
                Shader.UpdateUniform("lightPosition", sun.Position);
                Shader.UpdateUniform("lightColour", sun.Colour.ToVector3());
                obj.Draw();
            }
        }
    }
}
