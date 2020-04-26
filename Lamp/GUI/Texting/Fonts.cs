using Lamp.GUI.Texting.Basics;
using System.IO;

namespace Lamp.GUI.Texting
{
    public static class Fonts
    {
        public static readonly Font Arial = Font.LoadFont(new FileInfo("Resources/Fonts/default.png"), new FileInfo("Resources/Fonts/default.fnt"));
    }
}
