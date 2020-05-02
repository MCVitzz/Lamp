using Lamp.Rendering.Buffers;

namespace Lamp.Models
{
    public class Model
    {
        protected VAO Vao;

        public Model(ModelData data, BufferLayout layout)
        {
            Vao = new VAO(data, layout);
        }

        public virtual void Draw()
        {
            Vao.BindAll();
            Vao.EnablePointers();
            Vao.Draw();
        }
    }
}
