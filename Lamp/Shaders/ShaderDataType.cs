using OpenTK.Graphics.OpenGL4;

namespace Lamp.Shaders
{
    public class ShaderDataType
    {
        public static ShaderDataType NONE =  new ShaderDataType(0, 0,  0);
        public static ShaderDataType FLOAT = new ShaderDataType(4, 1,  VertexAttribPointerType.Float);
        public static ShaderDataType VEC2 =  new ShaderDataType(4, 2,  VertexAttribPointerType.Float);
        public static ShaderDataType VEC3 =  new ShaderDataType(4, 3,  VertexAttribPointerType.Float);
        public static ShaderDataType VEC4 =  new ShaderDataType(4, 4,  VertexAttribPointerType.Float);
        public static ShaderDataType MAT3 =  new ShaderDataType(4, 9,  VertexAttribPointerType.Float);
        public static ShaderDataType MAT4 =  new ShaderDataType(4, 16, VertexAttribPointerType.Float);

        private readonly int size;
        private readonly int elements;
        private readonly VertexAttribPointerType dataType;

        private ShaderDataType(int size, int elements, VertexAttribPointerType dataType)
        {
            this.size = size;
            this.elements = elements;
            this.dataType = dataType;
        }

        public int GetSize()
        {
            return size;
        }

        public VertexAttribPointerType GetDataType()
        {
            return dataType;
        }

        public int GetElements()
        {
            return elements;
        }
    }

}
