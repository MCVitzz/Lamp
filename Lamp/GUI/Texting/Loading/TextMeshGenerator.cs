using Lamp.GUI.Texting.Basics;
using Lamp.Rendering.Buffers;
using Lamp.Shaders;
using System.Collections.Generic;

namespace Lamp.GUI.Texting.Loading
{
    public class TextMeshGenerator
    {

        private static readonly int VERTS_PER_CHAR = 6;
        private static readonly int FLOATS_PER_VERT = 4;

        private readonly float SpaceWidth;

        private double CurserX;
        private double CurserY;
        private double ExtraSpace;
        private int Pointer;

        private static readonly BufferLayout Layout = new BufferLayout(new BufferElement[] {
            new BufferElement("position", ShaderDataType.VEC2),
            new BufferElement("textureCoords", ShaderDataType.VEC2),
        });

        public TextMeshGenerator(float spaceWidth)
        {
            SpaceWidth = spaceWidth;
        }

        public TextMesh GenerateTextMesh(Text text, List<Line> lines)
        {
            int vertexCount = CalcVertexCount(lines);
            float[] data = new float[vertexCount * FLOATS_PER_VERT];
            ResetCursors();
            for (int i = 0; i < lines.Count; i++)
            {
                Line line = lines[i];
                ApplyTextPositioning(text, line, i == lines.Count - 1);
                ProcessLine(text, line, data);
                CurserY += Text.LineHeightPixels * text.FontSize;
            }
            VAO2D vao = new VAO2D(data, Layout, vertexCount);
            return new TextMesh(vao, vertexCount, lines.Count);
        }

        private void ResetCursors()
        {
            CurserX = 0f;
            CurserY = 0f;
            Pointer = 0;
        }

        private void ProcessLine(Text text, Line line, float[] data)
        {
            foreach (Word word in line.Words)
            {
                ProcessWord(word, text.FontSize, data);
                CurserX += SpaceWidth * text.FontSize + ExtraSpace;
            }
        }

        private void ProcessWord(Word word, float fontSize, float[] data)
        {
            foreach (Character letter in word.Characters)
            {
                StoreCharMeshData(letter, fontSize, data);
                CurserX += letter.XAdvance * fontSize;
            }
        }

        private void ApplyTextPositioning(Text text, Line line, bool lastLine)
        {
            CurserX = 0;
            if (text.Alignment == Alignment.CENTER)
            {
                CurserX = (line.MaxPixelLength - line.LineLengthPixels) / 2;
            }
            else if (text.Alignment == Alignment.RIGHT)
            {
                CurserX = line.MaxPixelLength - line.LineLengthPixels;
            }
            if (text.Alignment == Alignment.JUSTIFY && !lastLine)
            {
                ExtraSpace = (line.MaxPixelLength - line.LineLengthPixels) / (line.Words.Count - 1);
            }
            else
            {
                ExtraSpace = 0;
            }
        }

        private void StoreCharMeshData(Character character, float fontSize, float[] data)
        {
            double x = CurserX + character.XOffset * fontSize;
            double y = CurserY + character.YOffset * fontSize;
            double maxX = x + character.SizeX * fontSize;
            double maxY = y + character.SizeY * fontSize;
            StoreVertexData(data, x, y, character.XTextureCoord, character.YTextureCoord);
            StoreVertexData(data, x, maxY, character.XTextureCoord, character.YTextureCoord);
            StoreVertexData(data, maxX, maxY, character.XMaxTextureCoord, character.YMaxTextureCoord);
            StoreVertexData(data, maxX, maxY, character.XMaxTextureCoord, character.YMaxTextureCoord);
            StoreVertexData(data, maxX, y, character.XMaxTextureCoord, character.YTextureCoord);
            StoreVertexData(data, x, y, character.XTextureCoord, character.YTextureCoord);
        }

        private int CalcVertexCount(List<Line> lines)
        {
            int charCount = 0;
            foreach (Line line in lines)
            {
                charCount += line.CharacterCount;
            }
            return charCount * VERTS_PER_CHAR;
        }

        private void StoreVertexData(float[] data, double x, double y, double texX, double texY)
        {
            data[Pointer++] = (float)x;
            data[Pointer++] = (float)y;
            data[Pointer++] = (float)texX;
            data[Pointer++] = (float)texY;
        }

    }

}
