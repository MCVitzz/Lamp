using Lamp.Rendering.Buffers;

namespace Lamp.GUI.Texting.Basics
{
    public class TextMesh
    {

        public readonly VAO2D Vao;
        public readonly int VertexCount;
        public readonly int LineCount;

        public TextMesh(VAO2D vao, int vertexCount, int lineCount)
        {
            Vao = vao;
            VertexCount = vertexCount;
            LineCount = lineCount;
        }

        public void Delete()
        {
            Vao.Destroy();
        }
    }

}
