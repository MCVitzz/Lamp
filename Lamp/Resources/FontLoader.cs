using SharpFont;

namespace Lamp.Resources
{
    public class FontLoader
    {

        public static Face LoadFont(Library lib, string filePath)
        {
            return new Face(lib, filePath);
        }
    }
}
