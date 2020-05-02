using Lamp.Core;
using OpenTK;
using QuickFont;
using System;
using System.Drawing;

namespace Lamp.GUI.Text
{
    public class TextComponent : Component
    {
        private readonly QFont Font;
        public string Value;
        private readonly QFontAlignment Alignment;
        private static Colour TextColour;

        public TextComponent(string value) : this(value, Fonts.GetFont("Arial", 14), QFontAlignment.Justify, new Colour(0, 0, 0)) { }

        public TextComponent(string value, QFont font, QFontAlignment alignment, Colour colour)
        {
            Value = value;
            Font = font;
            Alignment = alignment;
            TextColour = colour;
        }

        public QFontDrawingPrimitive Draw()
        {
            return PrintWithBounds(Font, Value, CreateRectangle(GetTransform()), Alignment, TextColour);
        }

        private static QFontDrawingPrimitive PrintWithBounds(QFont font, string text, RectangleF bounds, QFontAlignment alignment, Colour colour)
        {
            var dp = new QFontDrawingPrimitive(font);
            SizeF size = dp.Measure(text);
            bounds = CalcOffset(bounds, size);
            dp.Print(text, new Vector3(bounds.X, bounds.Y, 0), alignment, colour.ToDrawing());
            return dp;
        }

        public static RectangleF CalcOffset(RectangleF bounds, SizeF size)
        {
            bounds.X += (bounds.Width / 2) - (size.Width / 2);
            bounds.Y = bounds.Y - (bounds.Height / 2) + (size.Height / 2);
            return bounds;
        }

        public static RectangleF CreateRectangle(Vector4 transform)
        {
            int x = ChangeReferential(transform.X, true);
            int y = ChangeReferential(1 - transform.Y, false);
            int w = Math.Abs(ChangeReferential(transform.Z, true));
            int h = Math.Abs(ChangeReferential(transform.W, false));

            return new RectangleF(x, y, w, h);
        }

        public static int ChangeReferential(float n, bool horizontal)
        {
            //float a = -1 + (2 * n);
            //float size = horizontal ? GUIManager.DisplayWidth : GUIManager.DisplayHeight;
            //return (int)(a * size / 2);
            float size = horizontal ? GUIManager.DisplayWidth : GUIManager.DisplayHeight;
            return (int)(n * size);
        }
    }
}
