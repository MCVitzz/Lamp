package engine.renderer.buffers;

import engine.shaders.ShaderDataType;

public class BufferElement {
    public String name;
    public ShaderDataType type;
    public int size;
    public int offset;
    public boolean normalized;

    public BufferElement(String name, ShaderDataType type) {
        this(name, type, false);
    }

    public BufferElement(String name, ShaderDataType type, boolean normalized) {
        this.name = name;
        this.type = type;
        this.size = type.getSize();
        this.offset = 0;
        this.normalized = normalized;
    }
}