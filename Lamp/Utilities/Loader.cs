using OpenTK;
using System.Collections.Generic;
using System.IO;
using Assimp;
using Lamp.Models;
using System.Linq;

namespace Lamp.Utilities
{
    public class Loader
    {
        public static RawModelData LoadOBJ(string path)
        {
            var fs = new FileStream(path, FileMode.Open);
            RawModelData rawModelData = new RawModelData();
            using (var importer = new AssimpContext())
            {
                var scene = importer.ImportFileFromStream(fs, ".obj");
                var c = scene.Cameras;
                rawModelData.vertices = ToVectors(scene.Meshes[0].Vertices);
                rawModelData.normals = ToVectors(scene.Meshes[0].Normals);
                rawModelData.indices = scene.Meshes[0].GetIndices().Select(a => (ushort)a).ToList();
                rawModelData.uvs = ToVectors2D(scene.Meshes[0].TextureCoordinateChannels[0]);
            }
            fs.Close();
            fs.Dispose();
            return rawModelData;
        }

        public static List<Vector3> ToVectors(List<Vector3D> vecs)
        {
            List<Vector3> newList = new List<Vector3>();
            foreach(Vector3D vector in vecs)
            {
                newList.Add(new Vector3(vector.X, vector.Y, vector.Z));
            }
            return newList;
        }
        public static List<Vector2> ToVectors2D(List<Vector3D> vecs)
        {
            List<Vector2> newList = new List<Vector2>();
            foreach (Vector3D vector in vecs)
            {
                newList.Add(new Vector2(vector.X, vector.Y));
            }
            return newList;
        }

        public static float[] ParseLine(string line, int argCount)
        {
            string[] args = line.Split(' ');
            float[] numbers = new float[argCount];
            for (int i = 0; i < argCount; i++)
            {
                numbers[i] = float.Parse(args[i + 1]);
            }
            return numbers;
        }

        public static string[] ReadLines(string path)
        {
            List<string> lines = new List<string>();
            string line;
            using (StreamReader file = new StreamReader(path))
            {
                while ((line = file.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines.ToArray();
        }
    }
}
