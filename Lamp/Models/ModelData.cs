namespace Lamp.Models
{
    public struct ModelData
    {
        public ushort[] Indices;
        public float[] Vertices;

        public ModelData(float[] vertices, ushort[] indices)
        {
            Vertices = vertices;
            Indices = indices;
        }
    }
}
