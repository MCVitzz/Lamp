using Lamp.Shaders;

namespace Lamp.Rendering.Buffers
{
    public struct BufferElement
    {
        public string name;
        public ShaderDataType type;
        public int size;
        public int offset;
        public bool normalized;

        public BufferElement(string name, ShaderDataType type) : this(name, type, false) {}

        public BufferElement(string name, ShaderDataType type, bool normalized)
        {
            this.name = name;
            this.type = type;
            size = type.GetSize();
            offset = 0;
            this.normalized = normalized;
        }
    }
}
