using System.Collections.Generic;

namespace Lamp.Rendering.Buffers
{
    public class BufferLayout
    {
        private readonly List<BufferElement> elements;
        private int stride;

        public BufferLayout(BufferElement[] elements)
        {
            this.elements = new List<BufferElement>(elements);
            CalculateOffsetsAndStride();
        }

        public void CalculateOffsetsAndStride()
        {
            int offset = 0;
            stride = 0;
            for (int i = 0; i < elements.Count; i++)
            {
                BufferElement element = elements[i];
                int size = element.size * element.type.GetElements();
                element.offset = offset;
                offset += size;
                stride += size;
                elements[i] = element;
            }
        }

        public List<BufferElement> GetElements()
        {
            return elements;
        }

        public int GetStride()
        {
            return stride;
        }
    }
}
