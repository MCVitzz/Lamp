using OpenTK.Graphics.OpenGL4;

namespace Lamp.Rendering.Buffers
{
    public class VAO2D : VAO
    {
        public VAO2D(float[] vertices, BufferLayout layout, int size) : base()
        {
            Size = size;
            Allocate(vertices);
            BindAll();
            SetPointers(layout);
            EnablePointers();
        }

        public void Allocate(float[] vertices)
        {
            Vbo = new VBO();
            Vbo.Bind();
            Vbo.Allocate(vertices);
        }
        new public void BindAll()
        {
            Bind();
            Vbo.Bind();
        }

        new public void Draw()
        {
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, Size);
        }
    }
}
