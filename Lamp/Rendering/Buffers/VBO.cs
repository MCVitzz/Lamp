using OpenTK.Graphics.OpenGL4;

namespace Lamp.Rendering.Buffers
{
    public class VBO
    {
        private int Id;

        public VBO()
        {
            Id = GL.GenBuffer();
        }

        public void Allocate(float[] vertices)
        {
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertices.Length, vertices, BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, Id);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Destroy()
        {
            GL.DeleteBuffer(Id);
        }
    }
}
