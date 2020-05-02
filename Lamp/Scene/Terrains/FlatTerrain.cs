using Lamp.Core;
using Lamp.Models;
using Lamp.Rendering.Buffers;
using Lamp.Shaders;
using System.Collections.Generic;

namespace Lamp.Scene.Terrains
{
    public class FlatTerrain : GameObject
    {
        public Colour Colour;
        private static readonly BufferLayout Layout = new BufferLayout(new BufferElement[] { 
            new BufferElement("position", ShaderDataType.VEC3),
            new BufferElement("normal", ShaderDataType.VEC3),
        });

        public FlatTerrain(int side, Colour colour)
        {
            Model = new Model(GenerateModel(side), Layout);
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
    }
}
