//using Lamp.Core;
//using Lamp.GUI.Texting;
//using Lamp.Rendering;

//namespace Lamp.GUI.Components
//{
//    public class Text : Component
//    {
//        private string Value;
//        public readonly Font Font;
//        public int FontSize;
//        public Alignment Alignment;
//        private Colour Colour;
//        private Texture Texture;

//        public Text(string value) : base()
//        {
//            Value = value;
//            Font = new Font("AlexBrush-Regular", 150);
//            FontSize = 24;
//            Alignment = Alignment.LEFT;
//            Colour = new Colour(0, 0, 0);
//            BackgroundColour = new Colour(0, 0, 0, 0);
//            Texture = new Texture(Font.RenderString(value, Colour.ToDrawing(), BackgroundColour.ToDrawing()));
//        }

//        public void SetValue(string value)
//        {
//            if (value != Value)
//            {
//                Value = value;
//                RecalculateTexture();
//            }
//        }

//        public void BindTexture(int slot)
//        {
//            Texture.Bind(slot);
//        }

//        public void SetColour(Colour colour)
//        {
//            if (colour != Colour)
//            {
//                Colour = colour;
//                RecalculateTexture();
//            }
//        }

//        private void RecalculateTexture()
//        {
//            Texture = new Texture(Font.RenderString(Value, Colour.ToDrawing(), BackgroundColour.ToDrawing()));
//        }

//        public override void PrepareRender()
//        {
//            Texture.Bind(0);
//        }

//    }
//}

//using QuickFont;
//using QuickFont.Configuration;
//using OpenTK;
//using Lamp.GUI.Texting;
//using Lamp.Core;

//namespace Lamp.GUI
//{
//    public class Text : Component
//    {
//        private QFont Font;
//        public string Value;
//        private QFontDrawing Drawing;
//        public Alignment Alignment;
//        private QFontRenderOptions Options;
//        public int FontSize = 24;

//        public Text(string text) : this(text, Fonts.Arial, new Colour(0, 0, 0), QFontAlignment.Left)
//        {

//        }

//        public Text(string text, Colour colour) : this(text, Fonts.Arial, colour, QFontAlignment.Left)
//        {

//        }

//        public Text(string text, QFont font, Colour colour, QFontAlignment alignment)
//        {
//            Font = font;
//            Value = text;
//            Drawing = new QFontDrawing();
//            Alignment = alignment;
//            Options = new QFontRenderOptions { Colour = colour.ToDrawing(), DropShadowActive = false };
//        }

//        public void Draw()
//        {
//            Drawing.Print(Font, Value, new Vector3(Layout.X.GetPixels(), Layout.Y.GetPixels(), 0), Alignment, Options);
//        }
//    }
//}


using Lamp.Core;
using Lamp.GUI.Texting.Loading;

namespace Lamp.GUI.Texting.Basics
{
    public class Text : Component
    {

        public static readonly int LineHeightPixels = 25;
        //height of a line of text at font size 1 (UI scale 1)

        public readonly Font Font;
        public readonly float FontSize;
        public readonly Alignment Alignment;
        public readonly bool Scalable;

        public string Value;
        public Colour Colour;

        public TextMesh Mesh;

        public int LineCount;
        public float OriginalWidthPixels;

        public Text(string text) : this(text, Fonts.Arial, 16, Alignment.LEFT, new Colour(1, 0, 0), true) { }

        public Text(string text, Font font, float fontSize, Alignment alignment, Colour colour, bool scalable) : base()
        {
            Value = text;
            Font = font;
            FontSize = scalable ? fontSize : fontSize;
            Scalable = scalable;
            Alignment = alignment;
            Colour = colour;
        }

        public static TextBuilder NewText(string text)
        {
            return new TextBuilder(text);
        }

        public int[] GetClippingBounds()
        {
            return null;
        }


        public float GetTextScale()
        {
            float scale = 1;
            if (Scalable)
            {
                scale = Layout.W.GetPixels() * GUIManager.DisplayWidth / OriginalWidthPixels;
            }
            return scale;
        }

        public void Init()
        {
            OriginalWidthPixels = Layout.W.GetPixels() * GUIManager.DisplayWidth;
            Mesh = Font.InitText(this, OriginalWidthPixels);
        }

        public void SetText(string newText)
        {
            if (Value.Equals(newText))
            {
                return;
            }
            Value = newText;
            Init();
        }
    }
}