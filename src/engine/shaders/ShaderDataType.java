package engine.shaders;

import static org.lwjgl.opengl.GL11.GL_FLOAT;

public enum ShaderDataType {
    NONE(0, 0, 0), FLOAT(4, 1, GL_FLOAT), VEC2(4, 2, GL_FLOAT), VEC3(4, 3, GL_FLOAT), VEC4(4, 4, GL_FLOAT), MAT3(4, 9, GL_FLOAT), MAT4(4, 16, GL_FLOAT);

    private final int size;
    private final int elements;
    private final int dataType;

    ShaderDataType(int size, int elements, int dataType) {
        this.size = size;
        this.elements = elements;
        this.dataType = dataType;
    }

    public int getSize() {
        return this.size;
    }

    public int getDataType() {
        return this.dataType;
    }

    public int getElements() {
        return this.elements;
    }
}
