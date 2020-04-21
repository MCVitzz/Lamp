using Lamp.Models;
using Lamp.Rendering.Buffers;
using Lamp.Rendering.Camera;
using Lamp.Scene.Lights;
using Lamp.Shaders;

namespace Lamp.Scene
{
    public class Cube : GameObject
    {

        public static readonly BufferLayout Layout = new BufferLayout(new BufferElement[] {
            new BufferElement("position", ShaderDataType.VEC3),
            new BufferElement("textureCoords", ShaderDataType.VEC2),
            new BufferElement("normals", ShaderDataType.VEC3),
        });

        public Cube() : base()
        {
            Shader = new Shader("Resources/Shaders/Textured/Vertex.glsl", "Resources/Shaders/Textured/Fragment.glsl");
            Shader.AddUniform("modelMatrix");
            Shader.AddUniform("viewMatrix");
            Shader.AddUniform("projectionMatrix");
            Shader.AddUniform("lightPosition");
            Shader.AddUniform("lightColour");
        }

        public Cube(ModelData model) : this()
        {
            Vao = new VAO(model, Layout);
        }

        public Cube(VAO vao) : this()
        {
            Vao = vao;
        } 

        public override void Draw(ICamera camera, Light sun)
        {
            Shader.Bind();
            Shader.UpdateUniform("modelMatrix", Transform.GetMatrix());
            Shader.UpdateUniform("viewMatrix", camera.GetViewMatrix());
            Shader.UpdateUniform("projectionMatrix", camera.GetProjectionMatrix());
            Shader.UpdateUniform("lightPosition", sun.Position);
            Shader.UpdateUniform("lightColour", sun.Colour.ToVector3());
            Vao.BindAll();
            Vao.EnablePointers();
            Vao.Draw();
        }
    }
}
