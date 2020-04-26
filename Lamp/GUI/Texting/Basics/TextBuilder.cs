using Lamp.Core;

namespace Lamp.GUI.Texting.Basics
{
    public class TextBuilder
    {

        private static readonly float RelSizeFactor = 1280;

        private readonly string Text;

        private float TextSize = 1;
        private readonly Colour Colour = new Colour(1, 1, 1);
        private bool Scalable = false;
        private readonly Alignment Alignment = Alignment.LEFT;
        private readonly Font Font = Fonts.Arial;

        public TextBuilder(string text)
        {
            Text = text;
        }

        public TextBuilder WithRelativeFontSize(float size)
        {
            Scalable = true;
            TextSize = (size / RelSizeFactor) * GUIManager.DisplayWidth;
            return this;
        }

        public Text Create()
        {
            return new Text(Text, Font, TextSize, Alignment, Colour, Scalable);
        }

    }

}
