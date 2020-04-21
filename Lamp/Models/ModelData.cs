namespace Lamp.Models
{
    public struct ModelData
    {
        public readonly float[] Vertices;
        public readonly ushort[] Indices;

        public ModelData(float[] vertices, ushort[] indices)
        {
            Vertices = vertices;
            Indices = indices;
        }
    }
}
