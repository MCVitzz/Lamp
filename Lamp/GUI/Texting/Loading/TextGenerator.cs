using Lamp.GUI.Texting.Basics;
using System.Collections.Generic;

namespace Lamp.GUI.Texting.Loading
{
    public class TextGenerator
    {

        private readonly TextStructureGenerator StructureGenerator;
        private readonly TextMeshGenerator MeshGenerator;

        public TextGenerator(TextStructureGenerator structureGenerator, TextMeshGenerator meshGenerator)
        {
            StructureGenerator = structureGenerator;
            MeshGenerator = meshGenerator;
        }

        public TextMesh InitializeText(Text text, float maxLineLengthPix)
        {
            List<Line> textStructure = StructureGenerator.CreateStructure(text.Value, text.FontSize, maxLineLengthPix);
            TextMesh mesh = MeshGenerator.GenerateTextMesh(text, textStructure);
            return mesh;

        }


    }

}
