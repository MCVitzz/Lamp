using Lamp.GUI.Texting.Basics;
using Lamp.Rendering.Buffers;
using Lamp.Shaders;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Lamp.GUI.Texting
{
    public static class TextManager
    {
        private static List<Text> Texts;
        private static Shader Shader;
        private static readonly float[] Vertices = { 0, 0,   0, 0,
                                                     0, 1,   0, 1,
                                                     1, 0,   1, 0,
                                                     1, 1,   1, 1 };
        private static VAO2D Vao;
        private static readonly BufferLayout Layout = new BufferLayout(new BufferElement[] {
            new BufferElement("position", ShaderDataType.VEC2),
            new BufferElement("textureCoords", ShaderDataType.VEC2),
        });

        public static void Init()
        {
            Texts = new List<Text>();
            Shader = new Shader("Resources/Shaders/Text/Vertex.glsl", "Resources/Shaders/Text/Fragment.glsl");
            Vao = new VAO2D(Vertices, Layout, 4);
            Shader.AddUniform("transform");
            Shader.AddUniform("texturino");
        }

        public static void AddText(Text text)
        {
            Texts.Add(text);
        }

        public static void RenderTexts()
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Disable(EnableCap.DepthTest);
            foreach (Text text in Texts)
            {
                text.Mesh.Vao.Draw();
            }
            GL.Enable(EnableCap.DepthTest);
            GL.Disable(EnableCap.Blend);
        }
    }
}
