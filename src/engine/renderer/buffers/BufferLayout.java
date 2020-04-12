package engine.renderer.buffers;

import java.util.ArrayList;
import java.util.Arrays;

public class BufferLayout {
    private final ArrayList<BufferElement> elements;
    private int stride;

    public BufferLayout(BufferElement[] elements) {
        this.elements = new ArrayList<>(Arrays.asList(elements));
        calculateOffsetsAndStride();
    }

    public void calculateOffsetsAndStride() {
        int offset = 0;
        stride = 0;
        for (BufferElement element : elements) {
            element.offset = offset;
            offset += element.size;
            stride += (element.size * element.type.getElements());
        }
    }

    public ArrayList<BufferElement> getElements() {
        return this.elements;
    }

    public int getStride() {
        return this.stride;
    }
}
