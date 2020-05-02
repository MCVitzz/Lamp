using OpenTK;
using OpenTK.Graphics.OpenGL4;
using QuickFont;
using System.Collections.Generic;

namespace Lamp.GUI.Text
{
    public class TextManager
    {
        public static List<TextComponent> Texts = new List<TextComponent>();
        private readonly static QFontDrawing Drawing = new QFontDrawing();
        private static Matrix4 ProjectionMatrix;

        public static void OnResize(int width, int height)
        {
            ProjectionMatrix = Matrix4.CreateOrthographicOffCenter(0, width, 0, height, -1, 1);
        }

        public static void AddText(TextComponent text)
        {
            Texts.Add(text);
        }

        public static void RenderTexts()
        {
            GL.DepthFunc(DepthFunction.Lequal);
            Drawing.ProjectionMatrix = ProjectionMatrix;
            Drawing.DrawingPrimitives.Clear();
            foreach (TextComponent text in Texts)
            {
                Drawing.DrawingPrimitives.Add(text.Draw());
            }
            Drawing.RefreshBuffers();
            Drawing.Draw();
        }
    }
}
