﻿using Lamp.Resources;
using OpenTK.Graphics.OpenGL4;
using Serilog;
using System;
using System.IO;

namespace Lamp.Rendering
{
    public class Texture
    {
        public readonly int Id;

        public Texture(byte[] image)
        {
            int side = (int)Math.Sqrt(image.Length / 4);
            Id = GL.GenTexture();
            Bind();
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
            //GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        //public Texture(BmpData data)
        //{
        //    Id = GL.GenTexture();
        //    GL.TexImage2D(TextureTarget.Texture2D,
        //        0,
        //        PixelInternalFormat.Rgba,
        //        data.Width,
        //        data.Height,
        //        0,
        //        PixelFormat.Rgba,
        //        PixelType.UnsignedByte,
        //        data.Ptr);
        //    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
        //    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        //    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        //}

        public Texture(FileInfo info) : this(info.FullName)
        {

        }

        public Texture(string texturePath) : this(AssetManager.GetTexutre(texturePath).ToArray())
        {

        }

        public void Bind()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Id);
        }


        public void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
