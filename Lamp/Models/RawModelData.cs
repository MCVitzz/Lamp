using OpenTK;
using System.Collections.Generic;

namespace Lamp.Models
{
    public struct RawModelData
    {
        public List<Vector3> vertices;
        public List<Vector2> uvs;
        public List<Vector3> normals;
        public List<ushort> indices;

        public RawModelData(List<Vector3> vertices, List<Vector2> uvs, List<Vector3> normals, List<ushort> indices)
        {
            this.vertices = vertices;
            this.uvs = uvs;
            this.normals = normals;
            this.indices = indices;
        }
    }
}
