using Lamp.Models;
using OpenTK.Graphics.OpenGL4;

namespace Lamp.Rendering.Buffers
{
    public class VAO
    {
        private int Id;
        public VBO Vbo;
        public BufferLayout Layout;
        public IBO Ibo;
        private int Size;

        public VAO()
        {
            Id = GL.GenVertexArray();
        }

        public VAO(ModelData model, BufferLayout layout) : this()
        {
            Allocate(model);
            BindAll();
            SetPointers(layout);
            EnablePointers();
        }

        public void Allocate(ModelData model)
        {
            Vbo = new VBO();
            Ibo = new IBO();
            Vbo.Bind();
            Ibo.Bind();
            Vbo.Allocate(model.Vertices);
            Ibo.Allocate(model.Indices);
            Size = model.Indices.Length;
        }

        public void Draw()
        {
            GL.Enable(EnableCap.DepthTest);
            BindAll();
            GL.DrawElements(PrimitiveType.Triangles, Size, DrawElementsType.UnsignedShort, 0);
        }

        public void SetPointers(BufferLayout layout)
        {
            Layout = layout;
            BindAll();
            int i = 0;
            foreach(BufferElement element in layout.GetElements())
            {
                GL.VertexAttribPointer(
                    i,
                    element.type.GetElements(),
                    element.type.GetDataType(),
                    element.normalized,
                    layout.GetStride(),
                    element.offset);
                i++;
            }
        }

        public void EnablePointers()
        {
            for(int i = 0; i < Layout.GetElements().Count; i++)
            {
                GL.EnableVertexAttribArray(i);
            }
        }

        public void DisablePointers()
        {
            for (int i = 0; i < Layout.GetElements().Count; i++)
            {
                GL.DisableVertexAttribArray(i);
            }
        }

        public void BindAll()
        {
            Bind();
            Vbo.Bind();
            Ibo.Bind();
        }

        public void Bind()
        {
            GL.BindVertexArray(Id);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }

        public void Destroy()
        {
            GL.DeleteVertexArray(Id);
            Vbo.Destroy();
            Ibo.Destroy();
        }
    }
}
