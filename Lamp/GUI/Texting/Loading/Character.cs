namespace Lamp.GUI.Texting.Loading
{
    public class Character
    {

        public readonly int Id;
        public readonly double XTextureCoord;
        public readonly double YTextureCoord;
        public readonly double XMaxTextureCoord;
        public readonly double YMaxTextureCoord;
        public readonly double XOffset;
        public readonly double YOffset;
        public readonly double SizeX;
        public readonly double SizeY;
        public readonly double XAdvance;

        public Character(int id, double xTextureCoord, double yTextureCoord, double xTexSize, double yTexSize,
                double xOffset, double yOffset, double sizeX, double sizeY, double xAdvance)
        {
            Id = id;
            XTextureCoord = xTextureCoord;
            YTextureCoord = yTextureCoord;
            XOffset = xOffset;
            YOffset = yOffset;
            SizeX = sizeX;
            SizeY = sizeY;
            XMaxTextureCoord = xTexSize + xTextureCoord;
            YMaxTextureCoord = yTexSize + yTextureCoord;
            XAdvance = xAdvance;
        }
    }
}
