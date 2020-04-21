using OpenTK.Graphics.OpenGL4;

namespace Lamp.Rendering.Buffers
{
    public class IBO
    {
        private int Id;

        public IBO()
        {
            Id = GL.GenBuffer();
        }

        public void Allocate(ushort[] indices)
        {
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(ushort), indices, BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, Id);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }
        public void Destroy()
        {
            GL.DeleteBuffer(Id);
        }
    }
}
