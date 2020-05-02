using Lamp.Rendering;
using Lamp.Rendering.Buffers;
using OpenTK.Graphics.OpenGL4;

namespace Lamp.Models
{
    public class TexturedModel : Model
    {
        public Texture Texture;

        public TexturedModel(ModelData data, Texture texture, BufferLayout layout) : base(data, layout)
        {
            Texture = texture;
        }

        public override void Draw()
        {
            if(Texture.Id % 2 == 0)
            {
                Texture.Bind();
            }
            else
            {
                GL.BindTexture(TextureTarget.Texture2D, 0);
            }
            base.Draw();
        }
    }
}
