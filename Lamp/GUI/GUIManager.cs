using Lamp.GUI.Components;
using Lamp.GUI.Text;
using Lamp.Rendering.Buffers;
using Lamp.Shaders;
using OpenTK.Graphics.OpenGL4;
using SharpFont;
using System.Collections.Generic;

namespace Lamp.GUI
{
    public static class GUIManager
    {
        private static readonly float[] Vertices = { 0, 0, 0, 1, 1, 0, 1, 1 };
        private static VAO2D Vao;
        private static List<Component> Components;
        public static readonly MasterComponent MasterComponent = new MasterComponent();
        private static Shader Shader;
        public static float DisplayWidth, DisplayHeight;
        public static Library FontLibrary = new Library();

        private static readonly BufferLayout Layout = new BufferLayout(new BufferElement[] {
            new BufferElement("position", ShaderDataType.VEC2),
        });

        public static void Init()
        {
            Vao = new VAO2D(Vertices, Layout, 4);
            Shader = new Shader("Resources/Shaders/GUI/Vertex.glsl", "Resources/Shaders/GUI/Fragment.glsl");
            Shader.AddUniform("colour");
            Shader.AddUniform("transform");
            Components = new List<Component>();
        }

        public static void AddComponent(Component component)
        {
            component.Parent = MasterComponent;
            component.UpdateLayout();
            Components.Add(component);
        }

        public static void DrawComponents()
        {
            Shader.Bind();
            Vao.Bind();
            PrepareRendering();
            foreach (Component component in Components)
            {
                DrawComponent(component);
            }
            EndRendering();
            TextManager.RenderTexts();
        }

        private static void DrawComponent(Component component)
        {
            if(component.GetType() != typeof(TextComponent))
            {
                Shader.UpdateUniform("colour", component.BackgroundColour);
                Shader.UpdateUniform("transform", component.GetTransform());
                Vao.Draw();
            }
            foreach (Component child in component.Children)
            {
                DrawComponent(child);
            }
        }

        private static void PrepareRendering()
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Disable(EnableCap.DepthTest);
        }

        private static void EndRendering()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Disable(EnableCap.Blend);
        }

        public static void OnResize(int width, int height)
        {
            DisplayWidth = width;
            DisplayHeight = height;
            TextManager.OnResize(width, height);
        }
    }
}
