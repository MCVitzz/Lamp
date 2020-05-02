using QuickFont;
using QuickFont.Configuration;
using System.Collections.Generic;
using System.Drawing.Text;

namespace Lamp.GUI.Text
{
    public class Fonts
    {
        private static readonly InstalledFontCollection InstalledFontCollection = new InstalledFontCollection();
        public static Dictionary<string, QFont> Defaults = GetDefaults();

        public static QFont GetFont(string font, int size)
        {
            return new QFont(font, size, new QFontBuilderConfiguration());
        }

        public static Dictionary<string, QFont> GetDefaults()
        {
            var fonts = new Dictionary<string, QFont>();

            foreach (var fontFamily in InstalledFontCollection.Families)
            {
                // Don't load too many fonts
                if (fonts.Count > 15)
                    break;

                fonts.Add(fontFamily.Name, new QFont(fontFamily.Name, 14, new QFontBuilderConfiguration()));
            }
            return fonts;
        }
    }
}
