using Lamp.Models;
using OpenTK;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lamp.Resources
{
    public class AssetManager
    {
        public static List<byte> GetTexutre(string path)
        {
            //Load the image
            Image<Rgba32> image = SixLabors.ImageSharp.Image.Load<Rgba32>(path);

            //ImageSharp loads from the top-left pixel, whereas OpenGL loads from the bottom-left, causing the texture to be flipped vertically.
            //This will correct that, making the texture display properly.
            image.Mutate(x => x.Flip(FlipMode.Vertical));

            //Get an array of the pixels, in ImageSharp's internal format.
            Rgba32[] tempPixels = image.GetPixelSpan().ToArray();

            //Convert ImageSharp's format into a byte array, so we can use it with OpenGL.
            List<byte> pixels = new List<byte>();

            foreach (Rgba32 p in tempPixels)
            {
                pixels.Add(p.R);
                pixels.Add(p.G);
                pixels.Add(p.B);
                pixels.Add(p.A);
            }

            return pixels;
        }

        public static string Read(byte[] file, Encoding encoding)
        {
            return encoding.GetString(file);
        }

        public static string Read(byte[] file)
        {
            return Read(file, Encoding.UTF8);
        }

        public static string Read(string path, Encoding encoding)
        {
            string content = "";
            using (StreamReader stream = new StreamReader(path, encoding))
            {
                content = stream.ReadToEnd();
            }
            return content;
        }

        public static string Read(string path)
        {
            return Read(path, Encoding.UTF8);
        }

        public static ModelData LoadOBJ(string path)
        {
            List<int> vertexIndices = new List<int>();
            List<int> uvIndices = new List<int>();
            List<int> normalIndices = new List<int>();
            List<Vertex> vertices = new List<Vertex>();
            List<Vector3> normals = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();
            List<ushort> indices = new List<ushort>();

            string[] file = ReadLines(path);
            foreach (string line in file)
            {
                if (line.StartsWith("v "))
                {
                    string[] currentLine = line.Split(' ');
                    Vector3 vertex = new Vector3(float.Parse(currentLine[1]), float.Parse(currentLine[2]), float.Parse(currentLine[3]));
                    Vertex newVertex = new Vertex((ushort)vertices.Count, vertex);
                    vertices.Add(newVertex);
                }
                else if (line.StartsWith("vt "))
                {
                    float[] numbers = ParseLine(line, 2);
                    uvs.Add(new Vector2(numbers[0], numbers[1]));
                }
                else if (line.StartsWith("vn "))
                {
                    float[] numbers = ParseLine(line, 3);
                    normals.Add(new Vector3(numbers[0], numbers[1], numbers[2]));
                }
                else if (line.StartsWith("f "))
                {
                    string[] currentLine = line.Split(' ');
                    string[] vertex1 = currentLine[1].Split('/');
                    string[] vertex2 = currentLine[2].Split('/');
                    string[] vertex3 = currentLine[3].Split('/');
                    ProcessVertex(vertex1, vertices, indices);
                    ProcessVertex(vertex2, vertices, indices);
                    ProcessVertex(vertex3, vertices, indices);
                }
            }

            RemoveUnusedVertices(vertices);


            var data = ToFloats(vertices, uvs, normals);
            return new ModelData(data, indices.ToArray());

        }

        private static void ProcessVertex(string[] vertex, List<Vertex> vertices, List<ushort> indices)
        {
            ushort index = ushort.Parse(vertex[0]);
            index--;
            Vertex currentVertex = vertices[index];
            int textureIndex = int.Parse(vertex[1]) - 1;
            int normalIndex = int.Parse(vertex[2]) - 1;
            if (!currentVertex.IsSet())
            {
                currentVertex.TextureIndex = textureIndex;
                currentVertex.NormalIndex = normalIndex;
                indices.Add(index);
            }
            else
            {
                DealWithAlreadyProcessedVertex(currentVertex, textureIndex, normalIndex, indices, vertices);
            }

        }

        private static void DealWithAlreadyProcessedVertex(Vertex previousVertex, int newTextureIndex, int newNormalIndex, List<ushort> indices, List<Vertex> vertices)
        {
            if (previousVertex.HasSameTextureAndNormal(newTextureIndex, newNormalIndex))
            {
                indices.Add(previousVertex.Index);
            }
            else
            {
                Vertex anotherVertex = previousVertex.DuplicateVertex;
                if (anotherVertex != null)
                {
                    DealWithAlreadyProcessedVertex(anotherVertex, newTextureIndex, newNormalIndex, indices, vertices);
                }
                else
                {
                    Vertex duplicateVertex = new Vertex((ushort)vertices.Count, previousVertex.Position);
                    duplicateVertex.TextureIndex = newTextureIndex;
                    duplicateVertex.NormalIndex = newNormalIndex;
                    previousVertex.DuplicateVertex = duplicateVertex;
                    vertices.Add(duplicateVertex);
                    indices.Add(duplicateVertex.Index);
                }
            }
        }
        private static void RemoveUnusedVertices(List<Vertex> vertices)
        {
            foreach (Vertex vertex in vertices)
            {
                if (!vertex.IsSet())
                {
                    vertex.TextureIndex = 0;
                    vertex.NormalIndex = 0;
                }
            }
        }

        public static float[] ToFloats(List<Vertex> vertices, List<Vector2> uvs, List<Vector3> normals)
        {
            List<float> data = new List<float>();
            for (int i = 0; i < vertices.Count; i++)
            {
                Vertex currentVertex = vertices[i];
                Vector3 position = currentVertex.Position;
                Vector2 textureCoord = uvs[currentVertex.TextureIndex];
                Vector3 normalVector = normals[currentVertex.NormalIndex];
                data.Add(position.X);
                data.Add(position.Y);
                data.Add(position.Z);
                data.Add(textureCoord.X);
                data.Add(textureCoord.Y);
                data.Add(normalVector.X);
                data.Add(normalVector.Y);
                data.Add(normalVector.Z);
            }
            return data.ToArray();
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
