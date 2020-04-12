package engine.renderer.buffers;

import engine.utils.BufferUtil;

import static org.lwjgl.opengl.GL45.*;

public class IBO {

    private int id;

    public IBO() {
        id = glGenBuffers();
    }

    public void allocate(int[] data) {
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, id);
        glBufferData(GL_ELEMENT_ARRAY_BUFFER, BufferUtil.createFlippedIntBuffer(data), GL_STATIC_DRAW);
    }

    public void bind() {
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, id);
    }

    public void unbind() {
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
    }

    public void delete() {
        glDeleteBuffers(id);
    }
}
