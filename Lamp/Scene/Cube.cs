using Lamp.Models;
using Lamp.Rendering.Buffers;
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
        }

        public Cube(Model model) : this()
        {
            Model = model;
        }
    }
}
