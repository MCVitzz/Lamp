using Lamp.Core;
using Lamp.Models;
using Lamp.Rendering.Buffers;
using Lamp.Rendering.Camera;
using Lamp.Scene.Lights;
using Lamp.Shaders;
using System.Collections.Generic;

namespace Lamp.Scene.Terrains
{
    public class FlatTerrain : GameObject
    {
        private int Side;
        public Colour Colour;
        private static readonly BufferLayout Layout = new BufferLayout(new BufferElement[] { 
            new BufferElement("position", ShaderDataType.VEC3),
            new BufferElement("normal", ShaderDataType.VEC3),
        });

        public FlatTerrain(int side, Colour colour)
        {
            Side = side;
            Vao = new VAO(GenerateModel(side), Layout);
            Shader = new Shader("Resources/Shaders/Terrain/Vertex.glsl", "Resources/Shaders/Terrain/Fragment.glsl");
            Shader.AddUniform("modelMatrix");
            Shader.AddUniform("viewMatrix");
            Shader.AddUniform("projectionMatrix");
            Shader.AddUniform("terrainColour");
            Shader.AddUniform("lightColour");
            Shader.AddUniform("lightPosition");
            Colour = colour;
        }

        public ModelData GenerateModel(int side)
        {
            List<float> vertices = new List<float>(); //3 floats for each vertex
            ushort[] indices = { 0, 1, 2, 0, 3, 2 };

            vertices.Add(-side);
            vertices.Add(0);
            vertices.Add(-side);
            //Normal
            vertices.Add(0);
            vertices.Add(1);
            vertices.Add(0);

            vertices.Add(+side);
            vertices.Add(0);
            vertices.Add(-side);
            //Normal
            vertices.Add(0);
            vertices.Add(1);
            vertices.Add(0);

            vertices.Add(+side);
            vertices.Add(0);
            vertices.Add(+side);
            //Normal
            vertices.Add(0);
            vertices.Add(1);
            vertices.Add(0);

            vertices.Add(-side);
            vertices.Add(0);
            vertices.Add(+side);
            //Normal
            vertices.Add(0);
            vertices.Add(1);
            vertices.Add(0);

            return new ModelData(vertices.ToArray(), indices);
        }

        public override void Draw(ICamera camera, Light sun)
        {
            Shader.Bind();
            Shader.UpdateUniform("modelMatrix", Transform.GetMatrix());
            Shader.UpdateUniform("viewMatrix", camera.GetViewMatrix());
            Shader.UpdateUniform("projectionMatrix", camera.GetProjectionMatrix());
            Shader.UpdateUniform("terrainColour", Colour);
            Shader.UpdateUniform("lightColour", sun.Colour.ToVector3());
            Shader.UpdateUniform("lightPosition", sun.Position);
            Vao.BindAll();
            Vao.Draw();
        }
    }
}
