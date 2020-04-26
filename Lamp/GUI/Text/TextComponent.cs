using Lamp.Core;
using OpenTK;
using QuickFont;
using QuickFont.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;

namespace Lamp.GUI.Text
{
    public class TextComponent : Component
    {
        private QFont Font;
        private string Value;
        private QFontAlignment Alignment;
        private static Colour TextColour;

        public TextComponent(string value) : this(value, Fonts.GetFont("Arial", 8), QFontAlignment.Justify, new Colour(0, 0, 0)) { }

        public TextComponent(string value, QFont font, QFontAlignment alignment, Colour colour)
        {
            Value = value;
            Font = font;
            Alignment = alignment;
            TextColour = colour;
        }

        public QFontDrawingPrimitive Draw()
        {
            return PrintWithBounds(Font, Value, RectangleAsPixels(GetTransform()), Alignment, TextColour);
        }

        private static QFontDrawingPrimitive PrintWithBounds(QFont font, string text, RectangleF bounds, QFontAlignment alignment, Colour colour)
        {
            var dp = new QFontDrawingPrimitive(font);
            dp.Print(text, new Vector3(bounds.X, bounds.Y, 0), new SizeF(bounds.Width, bounds.Height), alignment, colour.ToDrawing());
            return dp;
        }

        public static RectangleF RectangleAsPixels(Vector4 transform)
        {
            int x = ChangeReferential(transform.X, true);
            int y = -ChangeReferential(transform.Y, false);
            int w = Math.Abs(ChangeReferential(transform.X + transform.Z, true));
            int h = Math.Abs(ChangeReferential((transform.Y + transform.W), false));
            
            return new RectangleF(x, y, w, h);
        }

        public static int ChangeReferential(float n, bool horizontal)
        {
            float a = -1 + (2 * n);
            float size = horizontal ? GUIManager.DisplayWidth : GUIManager.DisplayHeight;
            return (int)(a * size / 2);
        }
    }
}
