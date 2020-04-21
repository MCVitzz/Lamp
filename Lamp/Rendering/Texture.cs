using Lamp.Resources;
using OpenTK.Graphics.OpenGL4;
using System;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace Lamp.Rendering
{
    public class Texture
    {
        private readonly int Id;

        public Texture(string texturePath)
        {

            byte[] image = AssetManager.GetTexutre(texturePath).ToArray();
            int side = (int)Math.Sqrt(image.Length / 4);
            Id = GL.GenTexture();
            Bind(0);
            GL.TexImage2D(TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                side,
                side,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                image);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureLodBias, -10);
        }

        public void Bind(int slot)
        {
            GL.BindTextureUnit(slot, Id);
            //GL.ActiveTexture(TextureUnit.Texture0);
            //GL.BindTexture(TextureTarget.Texture2D, Id);
        }
        public void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
