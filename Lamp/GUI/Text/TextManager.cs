using OpenTK;
using OpenTK.Graphics.OpenGL4;
using QuickFont;
using System.Collections.Generic;

namespace Lamp.GUI.Text
{
    public class TextManager
    {
        public static List<TextComponent> Texts = new List<TextComponent>();
        private static QFontDrawing Drawing = new QFontDrawing();
        private static Matrix4 ProjectionMatrix;

        public static void OnResize(int width, int height)
        {
            ProjectionMatrix = Matrix4.CreateOrthographic(width, height, 0, 1000);
        }

        public static void AddText(TextComponent text)
        {
            Texts.Add(text);
        }

        public static void RenderTexts()
        {
            Drawing.ProjectionMatrix = ProjectionMatrix;
            Drawing.DrawingPrimitives.Clear();
            foreach(TextComponent text in Texts)
            {
                Drawing.DrawingPrimitives.Add(text.Draw());
            }
            Drawing.RefreshBuffers();
            Drawing.Draw();
        }
    }
}
