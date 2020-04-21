using Lamp.Scene.Lights;
using Lamp.Core;
using Lamp.Models;
using Lamp.Rendering.Buffers;
using Lamp.Rendering.Camera;
using Lamp.Shaders;

namespace Lamp.Scene.Terrains
{
    public class FlatTerrain : GameObject
    {
        private int Side;
        public Colour Colour;
        private static readonly BufferLayout Layout = new BufferLayout(new BufferElement[] { 
            new BufferElement("position", ShaderDataType.VEC3)
        });

        public FlatTerrain(int side, Colour colour)
        {
            Side = side;
            Vao = new VAO(GenerateModel(side), Layout);
            Shader = new Shader("Resources/Shaders/Terrain/Vertex.glsl", "Resources/Shaders/Terrain/Fragment.glsl");
            Shader.AddUniform("modelMatrix");
            Shader.AddUniform("viewMatrix");
            Shader.AddUniform("projectionMatrix");
            Shader.AddUniform("colour");
            Colour = colour;
        }

        public ModelData GenerateModel(int side)
        {
            float[] vertices = new float[3 * 4]; //3 floats for each vertex
            ushort[] indices = { 0, 1, 2, 0, 3, 2 };

            vertices[0] = -side;
            vertices[1] = 0;
            vertices[2] = -side;

            vertices[3] = +side;
            vertices[4] = 0;
            vertices[5] = -side;

            vertices[6] = +side;
            vertices[7] = 0;
            vertices[8] = +side;

            vertices[9] = -side;
            vertices[10] = 0;
            vertices[11] = +side;

            return new ModelData(vertices, indices);
        }

        public override void Draw(ICamera camera, Light sun)
        {
            Shader.Bind();
            Shader.UpdateUniform("modelMatrix", Transform.GetMatrix());
            Shader.UpdateUniform("viewMatrix", camera.GetViewMatrix());
            Shader.UpdateUniform("projectionMatrix", camera.GetProjectionMatrix());
            Shader.UpdateUniform("colour", Colour);
            Vao.BindAll();
            Vao.Draw();
        }
    }
}
