using OpenTK.Graphics.OpenGL4;

namespace Lamp.Rendering.Buffers
{
    public class GuiVAO
    {
        private readonly int Id;
        private int Size;
        private VBO Vbo;
        private BufferLayout Layout;

        public GuiVAO(float[] vertices, BufferLayout layout)
        {
            Id = GL.GenVertexArray();
            Allocate(vertices);
            Bind();
            SetPointers(layout);
            EnablePointers();
        }

        public void Allocate(float[] vertices)
        {
            Vbo = new VBO();
            Vbo.Bind();
            Vbo.Allocate(vertices);
            Size = 4;
        }

        public void SetPointers(BufferLayout layout)
        {
            Layout = layout;
            BindAll();
            int i = 0;
            foreach (BufferElement element in layout.GetElements())
            {
                GL.VertexAttribPointer(i,
                    element.type.GetElements(),
                    element.type.GetDataType(),
                    element.normalized, layout.GetStride(),
                    element.offset);
                i++;
            }
        }

        public void EnablePointers()
        {
            for (int i = 0; i < Layout.GetElements().Count; i++)
            {
                GL.EnableVertexAttribArray(i);
            }
        }

        public void Draw()
        {
            BindAll();
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, Size);
        }

        public void BindAll()
        {
            Bind();
            Vbo.Bind();
        }

        public void Bind()
        {
            GL.BindVertexArray(Id);
        }
    }
}
