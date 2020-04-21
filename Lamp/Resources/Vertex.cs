using OpenTK;

namespace Lamp.Resources
{
    public class Vertex
    {

        private const int NO_INDEX = -1;

        public Vector3 Position;
        public int TextureIndex = NO_INDEX;
        public int NormalIndex = NO_INDEX;
        public Vertex DuplicateVertex = null;
        public ushort Index;
        public float Length;

        public Vertex(ushort index, Vector3 position)
        {
            Index = index;
            Position = position;
            Length = position.Length;
        }

        public bool IsSet()
        {
            return TextureIndex != NO_INDEX && NormalIndex != NO_INDEX;
        }

        public bool HasSameTextureAndNormal(int textureIndexOther, int normalIndexOther)
        {
            return textureIndexOther == TextureIndex && normalIndexOther == NormalIndex;
        }
    }
}
